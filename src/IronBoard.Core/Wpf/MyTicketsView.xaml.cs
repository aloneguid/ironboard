﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for MyTicketsView.xaml
   /// </summary>
   public partial class MyTicketsView : UserControl, IMyTicketsView
   {
      private readonly MyTicketsPresenter _presenter;

      public MyTicketsView()
      {
         InitializeComponent();

         _presenter = new MyTicketsPresenter(this);
      }

      public void RefreshView()
      {
         Progress.IsInProgress = true;
         _presenter.ReloadData();   
      }

      public void SetSelection(IEnumerable<WorkItem> items)
      {
         
      }

      private void Refresh_Click(object sender, RoutedEventArgs e)
      {
         RefreshView();
      }

      public void UpdateList(IEnumerable<Review> myTickets, Exception error)
      {
         Dispatcher.Push(() =>
         {
            Progress.IsInProgress = false;
            Tickets.ItemsSource = myTickets;
         });
      }

      private void UpdateTicketMenu_OnClick(object sender, RoutedEventArgs e)
      {
         //
      }

      private void Tickets_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         Review r = Tickets.SelectedItem as Review;
         if (r != null)
         {
            UpdateTicketMenu.Header = "update #" + r.Id;
         }
      }

      private void Tickets_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         Review r = Tickets.SelectedItem as Review;
         if (r != null)
         {
            _presenter.OpenInBrowser(r);
         }
      }
   }
}
