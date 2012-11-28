using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for ReviewDetails.xaml
   /// </summary>
   public partial class ReviewDetails : Window, IReviewDetailsView
   {
      private readonly Review _review;
      private readonly long _fromRev;
      private readonly long _toRev;
      private readonly ReviewDetailsPresenter _presenter;

      public ReviewDetails(string title, Review review, long fromRev, long toRev)
      {
         if (review == null) throw new ArgumentNullException("review");

         _review = review;
         _fromRev = fromRev;
         _toRev = toRev;
         InitializeComponent();
         Title = title;
         Background = IbApplication.BackgroundBrush;
         Progress.ProgressTitle = Strings.ReviewDetails_GeneratingDiff;

         _presenter = new ReviewDetailsPresenter(this);
         InitialiseData();
      }

      private void InitialiseData()
      {
         if (!DesignerProperties.GetIsInDesignMode(this))
         {
            SummaryText.Text = _review.Subject;
            DescriptionText.Text = _review.Description;
            BugsText.Text = _review.BugsClosed;
            TestingText.Text = _review.TestingDone;

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

      private void FillReview()
      {
         _review.TargetUsers.Clear();
         _review.TargetGroups.Clear();
         if (Users.Reviewers != null)
         {
            foreach (User u in Users.Reviewers) _review.TargetUsers.Add(u);
         }
         if (Groups.Reviewers != null)
         {
            foreach (UserGroup g in Groups.Reviewers) _review.TargetGroups.Add(g);
         }         
      }

      private void Post_OnClick(object sender, RoutedEventArgs e)
      {
         FillReview();
         //todo: post this shit
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
