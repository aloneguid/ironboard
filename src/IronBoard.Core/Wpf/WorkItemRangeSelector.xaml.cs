﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
      private readonly WorkItemRangeSelectorPresenter _presenter;
      private readonly Dictionary<WorkItem, int> _itemOrder = new Dictionary<WorkItem, int>(); 

      public WorkItemRangeSelector()
      {
         InitializeComponent();

         _presenter = new WorkItemRangeSelectorPresenter(this);
         Warning.Visibility = Visibility.Collapsed;
         CommandLine.Content = null;
      }

      public void RefreshView()
      {
         if (MaxItems > 0 && WorkItems != null)
         {
            //ToolBar.IsEnabled = false;
            Progress.IsInProgress = true;
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
                        //ToolBar.IsEnabled = true;
                        Progress.IsInProgress = false;
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
         bool skipped;
         List<WorkItem> continuous = SelectCoutinuousSelectedItems(out skipped).ToList();
         Tuple<int, int> range = _presenter.GetRange(continuous);
         Review review = new Review();
         _presenter.ExtractBasicMetadata(continuous, review);
         var detailWindow = new ReviewDetails(Strings.ReviewDetails_NewTicket, review, range.Item1, range.Item2);
         detailWindow.ShowDialog();
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

         Tuple<int, int> range = _presenter.GetRange(continuous);
         CommandLine.Content = _presenter.GetCommandLine(range);
      }
   }
}
