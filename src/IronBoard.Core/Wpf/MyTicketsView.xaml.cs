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

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for MyTicketsView.xaml
   /// </summary>
   public partial class MyTicketsView : UserControl, IMyTicketsView
   {
      private readonly MyTicketsPresenter _presenter;
      private IEnumerable<WorkItem> _items;
      private RevisionRange _range;

      public MyTicketsView()
      {
         InitializeComponent();
         UpdateMenus(null);
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

      public void SetSelection(IEnumerable<WorkItem> items, RevisionRange range)
      {
         this._items = items;
         _range = range;

         UpdateMenus(range);
      }

      private void UpdateMenus(RevisionRange range)
      {
         bool selected = range != null;
         TopHint.Content = !selected ? null : string.Format(Strings.MyTickets_TopHint, range.From, range.To);

         MenuUpdateTicketMenu.IsEnabled = selected;
         if (!selected)
         {
            MenuUpdateTicketMenu.Header = Strings.MenuUpdate_NoTickets;
         }
         else
         {
            string header = string.Format(Strings.MyTickets_UpdateTicketMenu, _range.From, _range.To);
            MenuUpdateTicketMenu.Header = header;
         }
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

      public void UpdateBusyStatus(string busyMessage)
      {
         Dispatcher.Push(() =>
            {
               Progress.IsBusy = true;
               Progress.BusyContent = busyMessage;
            });
      }

      public void FinishTicketUpdate(Exception ex)
      {
         Dispatcher.Push(() =>
            {
               Progress.IsBusy = false;
               if (ex != null)
               {
                  Messages.ShowError(ex);
               }
            });
      }

      private void UpdateTicketMenu_OnClick(object sender, RoutedEventArgs e)
      {
         var r = Tickets.SelectedItem as MyTicketData;
         if (r != null)
         {
            _presenter.UpdateTicket(r.R, _range);
         }
      }

      private void Tickets_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         var r = Tickets.SelectedItem as MyTicketData;
         if (r != null)
         {
            UpdateMenus(_range);
         }
      }

      private void WebOpenSelectedTicket(bool external)
      {
         var r = Tickets.SelectedItem as MyTicketData;
         if (r != null)
         {
            _presenter.OpenInBrowser(r.R, external);
         }         
      }

      private void Tickets_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         WebOpenSelectedTicket(false);
      }

      private void OpenInWebBrowserMenuItemClick(object sender, RoutedEventArgs e)
      {
         WebOpenSelectedTicket(false);
      }

      private void OpenInExternalWebBrowserClick(object sender, RoutedEventArgs e)
      {
         WebOpenSelectedTicket(true);
      }

      private void DeletePermanentlyClick(object sender, RoutedEventArgs e)
      {
         var r = Tickets.SelectedItem as MyTicketData;
         if (r != null)
         {
            _presenter.Delete(r.R);
            RefreshView();
         }
      }
   }
}
