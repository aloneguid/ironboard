using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.Core.Wpf.Data;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for MyTicketsView.xaml
   /// </summary>
   public partial class MyTicketsView : UserControl, IMyTicketsView
   {
      private readonly MyTicketsPresenter _presenter;
      private IEnumerable<WorkItem> items;
      private int rmin, rmax;

      public MyTicketsView()
      {
         InitializeComponent();
         LoadError.Visibility = Visibility.Collapsed;
         TopHint.Content = null;
         _presenter = new MyTicketsPresenter(this);
      }

      public void RefreshView()
      {
         Progress.BusyContent = Strings.MyTickets_LoadingTickets;
         Progress.IsBusy = true;
         LoadError.Visibility = Visibility.Collapsed;
         _presenter.ReloadData();   
      }

      public void SetSelection(IEnumerable<WorkItem> items, int rmin, int rmax)
      {
         this.items = items;
         this.rmin = rmin;
         this.rmax = rmax;

         TopHint.Content = items == null ? null : string.Format(Strings.MyTickets_TopHint, rmin, rmax);
      }
      
      private void Refresh_Click(object sender, RoutedEventArgs e)
      {
         RefreshView();
      }

      public void UpdateList(IEnumerable<MyTicketData> myTickets, Exception error)
      {
         Dispatcher.Push(() =>
         {
            Progress.IsBusy = false;
            if(error != null)
            {
               string msg = string.Format(Strings.MyTickets_LoadError, error.Message);
               LoadError.Content = msg;
               LoadError.Visibility = Visibility.Visible;
            }
            Tickets.ItemsSource = myTickets;
         });
      }

      private void UpdateTicketMenu_OnClick(object sender, RoutedEventArgs e)
      {
         //
      }

      private void Tickets_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         var r = Tickets.SelectedItem as MyTicketData;
         if (r != null)
         {
            string header = string.Format(Strings.MyTickets_UpdateTicketMenu, r.R.Id, rmin, rmax);
            UpdateTicketMenu.Header = header;
         }
      }

      private void WebOpenSelectedTicket()
      {
         var r = Tickets.SelectedItem as MyTicketData;
         if (r != null)
         {
            _presenter.OpenInBrowser(r.R);
         }         
      }

      private void Tickets_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         WebOpenSelectedTicket();
      }

      private void OpenInWebBrowserMenuItemClick(object sender, RoutedEventArgs e)
      {
         WebOpenSelectedTicket();
      }
   }
}
