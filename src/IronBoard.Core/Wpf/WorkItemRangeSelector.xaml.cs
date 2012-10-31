using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for WorkItemRangeSelector.xaml
   /// </summary>
   public partial class WorkItemRangeSelector : UserControl, IWorkItemRangeSelectorView
   {
      private readonly WorkItemRangeSelectorPresenter _presenter;

      public WorkItemRangeSelector()
      {
         InitializeComponent();

         _presenter = new WorkItemRangeSelectorPresenter(this);
      }

      private void RefreshView()
      {

         WorkItems.ItemsSource = _presenter.GetCurrentWorkItems(10);
      }
   }
}
