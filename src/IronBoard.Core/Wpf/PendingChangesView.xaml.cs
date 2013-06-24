using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;
using System.Collections;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for PendingChangesView.xaml
   /// </summary>
   public partial class PendingChangesView : UserControl, IPendingChangesView
   {
      private readonly PendingChangesPresenter _presenter;

      public PendingChangesView()
      {
         InitializeComponent();

         _presenter = new PendingChangesPresenter(this);
      }

      public void RefreshView()
      {
         IEnumerable<LocalWorkItem> work = _presenter.GetPendingChanges();

         Dispatcher.Push(() =>
            {
               IList selectedItems = ChangesList.SelectedItems;
               IList<LocalWorkItem> prevSelectedItems = selectedItems == null ? null : selectedItems.Cast<LocalWorkItem>().ToList();
               ChangesList.ItemsSource = work; // Here selected items clearing.
               ItemCollection items = ChangesList.Items;
               
               // synchronize selected items
               if (prevSelectedItems != null && items != null)
               {
                   foreach (LocalWorkItem prevSelItem in prevSelectedItems)
                   {
                       foreach (var item in items)
                       {
                           if ((item as LocalWorkItem).Path.Equals(prevSelItem.Path))
                           {
                               selectedItems.Add(item);
                           }
                       }
                   }
               }
               ChangesList.IsEnabled = items != null;
               NoChanges.Visibility = (items == null) ? Visibility.Visible : Visibility.Collapsed;

               RefreshPreCommitReviewButton();
            });
      }

      private void PreCommitReview_OnClick(object sender, RoutedEventArgs e)
      {
         IEnumerable<LocalWorkItem> selectedItems = ChangesList.SelectedItems.Cast<LocalWorkItem>();
         var review = new Review();
         var detailWindow = new ReviewDetails(_presenter.GetDetailsTitle(),
            review,
            delegate() { return IbApplication.SvnRepository.GetDiff(selectedItems); });
         detailWindow.ShowDialog();
         if (review.Id != 0) _presenter.OpenInBrowser(review);
      }

      private void ChangesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         RefreshPreCommitReviewButton();
      }

      private void RefreshPreCommitReviewButton()
      {
         IList selectedItems = ChangesList.SelectedItems;
         PreCommitReview.IsEnabled = selectedItems != null && selectedItems.Count > 0;
      }
   }
}
