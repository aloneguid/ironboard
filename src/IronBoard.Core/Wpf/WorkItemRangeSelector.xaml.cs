using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using IronBoard.Core.Application;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for WorkItemRangeSelector.xaml
   /// </summary>
   public partial class WorkItemRangeSelector : UserControl, IWorkItemRangeSelectorView
   {
      public event Action<IEnumerable<WorkItem>, RevisionRange> SelectectionChanged;

      private readonly WorkItemRangeSelectorPresenter _presenter;

      public WorkItemRangeSelector()
      {
         InitializeComponent();

         _presenter = new WorkItemRangeSelectorPresenter(this);
         Warning.Visibility = Visibility.Collapsed;
      }

      public void RefreshView()
      {
         if (MaxItems > 0 && WorkItems != null)
         {
            //ToolBar.IsEnabled = false;
            Progress.IsBusy = true;
            int maxItems = MaxItems;
            _presenter.ReloadData(maxItems);
         }
      }

      public void UpdateList(Exception ex, IEnumerable<WorkItem> items)
      {
         Dispatcher.Push(() =>
         {
            Progress.IsBusy = false;
            WorkItems.ItemsSource = items;
         });
      }

      private int MaxItems
      {
         get
         {
            return MaxRevisions.SelectedValue == null
                      ? 0
                      : int.Parse(((ComboBoxItem) MaxRevisions.SelectedValue).Content.ToString());
         }
      }

      private void RefreshClick(object sender, RoutedEventArgs e)
      {
         RefreshView();
      }

      private void MaxRevisions_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         RefreshView();
      }

      private void PostReview_Click(object sender, RoutedEventArgs e)
      {
         bool skipped;
         List<WorkItem> continuous = SelectCoutinuousSelectedItems(out skipped).ToList();
         RevisionRange range = GenericRepository.GetRange(continuous);
         var review = new Review();
         _presenter.ExtractBasicMetadata(continuous, review);
         var detailWindow = new ReviewDetails(_presenter.GetDetailsTitle(),
            review,
            () => IbApplication.CodeRepository.GetDiff(range));
         detailWindow.ShowDialog();
         if(review.Id != 0) _presenter.OpenInBrowser(review);
      }

      private IEnumerable<WorkItem> SelectCoutinuousSelectedItems(out bool skipped)
      {
         if(WorkItems.SelectedItems != null && WorkItems.Items != null)
         {
            return _presenter.SelectContinuousItems(WorkItems.Items, WorkItems.SelectedItems, out skipped);
         }

         skipped = false;
         return null;
      }

      private void WorkItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         PostReview.IsEnabled = WorkItems.SelectedItems != null && WorkItems.SelectedItems.Count > 0;

         bool skipped;
         List<WorkItem> continuous = SelectCoutinuousSelectedItems(out skipped).ToList();
         Warning.Visibility = skipped ? Visibility.Visible : Visibility.Collapsed;

         RevisionRange range = GenericRepository.GetRange(continuous);

         if(SelectectionChanged != null)
         {
            SelectectionChanged(continuous, range);
         }
      }
   }
}
