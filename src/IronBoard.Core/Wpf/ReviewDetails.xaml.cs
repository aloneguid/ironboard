using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for ReviewDetails.xaml
   /// </summary>
   public partial class ReviewDetails : Window, IReviewDetailsView
   {
      private readonly long _fromRev;
      private readonly long _toRev;
      private readonly ReviewDetailsPresenter _presenter;

      public ReviewDetails(string title, long fromRev, long toRev)
      {
         _fromRev = fromRev;
         _toRev = toRev;
         InitializeComponent();
         Title = title;

         _presenter = new ReviewDetailsPresenter(this);
         InitialiseData();
      }

      private void InitialiseData()
      {
         if (!DesignerProperties.GetIsInDesignMode(this))
         {
            Progress.IsInProgress = true;
            Task.Factory.StartNew(() =>
               {
                  try
                  {
                     _presenter.GenerateDiff(_fromRev, _toRev);
                  }
                  catch (Exception ex)
                  {
                  }

                  Dispatcher.Push(() =>
                     {
                        Progress.IsInProgress = false;
                     });
               });
         }
      }

      public string Description
      {
         set { DescriptionText.Text = value; }
      }

      private void Post_OnClick(object sender, RoutedEventArgs e)
      {
         //
      }

      private void Cancel_OnClick(object sender, RoutedEventArgs e)
      {
         //
      }

      //
   }
}
