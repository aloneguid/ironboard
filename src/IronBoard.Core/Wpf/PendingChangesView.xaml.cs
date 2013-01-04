using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;

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
               ChangesList.ItemsSource = work;
               ChangesList.IsEnabled = work != null;
               NoChanges.Visibility = (work == null) ? Visibility.Visible : Visibility.Collapsed;

               PreCommitReview.IsEnabled = false;
            });
      }

      private void PreCommitReview_OnClick(object sender, RoutedEventArgs e)
      {

      }

      private void ChangesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         
      }
   }
}
