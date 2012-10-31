using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.Core.Wpf.Data;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for WorkItemRangeSelector.xaml
   /// </summary>
   public partial class WorkItemRangeSelector : UserControl, IWorkItemRangeSelectorView
   {
      private readonly WorkItemRangeSelectorPresenter _presenter;
      private readonly Dictionary<WorkItem, int> _itemOrder = new Dictionary<WorkItem, int>(); 

      public WorkItemRangeSelector()
      {
         InitializeComponent();

         _presenter = new WorkItemRangeSelectorPresenter(this);
      }

      public void RefreshView()
      {
         if (MaxItems > 0 && WorkItems != null)
         {
            ToolBar.IsEnabled = false;
            int maxItems = MaxItems;
            Task.Factory.StartNew(() =>
               {
                  IEnumerable<WorkItem> items = null;
                  try
                  {
                     items = _presenter.GetCurrentWorkItems(maxItems);
                     _itemOrder.Clear();
                     if (items != null)
                     {
                        int i = 0;
                        foreach(WorkItem wi in items)
                        {
                           _itemOrder[wi] = i++;
                        }
                     }
                  }
                  catch(Exception ex)
                  {
                     //todo: show error
                  }
                  Dispatcher.Push(() =>
                     {
                        ToolBar.IsEnabled = true;
                        WorkItems.ItemsSource = items;
                     });
               });
         }
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

      }

      private void WorkItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         PostReview.IsEnabled = WorkItems.SelectedItems != null && WorkItems.SelectedItems.Count > 0;


      }
   }
}
