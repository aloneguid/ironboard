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
            ViewDiff.IsEnabled = false;
            DiffError.Visibility = Visibility.Collapsed;
            UpdateButtons();
            Task.Factory.StartNew(() =>
               {
                  Exception ex = null;
                  try
                  {
                     _presenter.GenerateDiff(_fromRev, _toRev);
                  }
                  catch (Exception ex1)
                  {
                     ex = ex1;
                  }

                  Dispatcher.Push(() =>
                     {
                        Progress.IsInProgress = false;
                        if(ex == null)
                        {
                           ViewDiff.IsEnabled = true;
                        }
                        else
                        {
                           DiffError.Content = string.Format(Strings.ReviewDetails_GenerateDiff_Error, ex.Message);
                           DiffError.Visibility = Visibility.Visible;
                        }
                        UpdateButtons();
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
         Close();
      }

      private void Cancel_OnClick(object sender, RoutedEventArgs e)
      {
         Close();
      }

      private void UpdateButtons()
      {
         bool enoughData = SummaryText.Text.Trim().Length > 0 &&
                           DescriptionText.Text.Trim().Length > 0 &&
                           TestingText.Text.Trim().Length > 0;

         Cancel.IsEnabled = !Progress.IsInProgress;
         Post.IsEnabled = enoughData && !Progress.IsInProgress && ViewDiff.IsEnabled;
      }

      private void ViewDiffClick(object sender, RoutedEventArgs e)
      {
         _presenter.OpenLastDiff();
      }

      private void FieldTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
      {
         UpdateButtons();
      }
   }
}
