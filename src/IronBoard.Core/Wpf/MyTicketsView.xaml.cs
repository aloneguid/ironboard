using System;
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
         Progress.IsInProgress = true;
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

      public void UpdateList(IEnumerable<Review> myTickets, Exception error)
      {
         Dispatcher.Push(() =>
         {
            Progress.IsInProgress = false;
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
         Review r = Tickets.SelectedItem as Review;
         if (r != null)
         {
            string header = string.Format(Strings.MyTickets_UpdateTicketMenu, r.Id, rmin, rmax);
            UpdateTicketMenu.Header = header;
         }
      }

      private void WebOpenSelectedTicket()
      {
         var r = Tickets.SelectedItem as Review;
         if (r != null)
         {
            _presenter.OpenInBrowser(r);
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
