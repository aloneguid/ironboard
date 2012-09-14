using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.WinForms
{
   public partial class PostCommitReviewForm : Form, IPostCommitReviewView
   {
      private PostCommitReviewPresenter _presenter;
      private readonly Review _review = new Review();

      public PostCommitReviewForm()
      {
         InitializeComponent();
         Shown += PostCommitReviewForm_Shown;
      }

      private void InitialiseData()
      {
         if (!DesignMode)
         {
            _presenter = new PostCommitReviewPresenter(this);

            UiScheduler.InitializeUiContext();

            SvnUri.Text = _presenter.SvnRepositoryUri;
            Progress.Text = "hi :)";
            WorkItems.SelectedWorkItemsChanged += WorkItems_SelectedWorkItemsChanged;
            WorkItems.WorkItemsInvalidated += WorkItems_WorkItemsInvalidated;
         }
      }

      void WorkItems_WorkItemsInvalidated(object obj)
      {
         ListRevisions();
      }

      void WorkItems_SelectedWorkItemsChanged(List<WorkItem> obj)
      {
         string txt = _presenter.ProduceDescription(obj);
         if (txt != null)
         {
            _review.Description = txt;
            Review.Display(_review);
         }

         ValidateCanPost();
      }

      void PostCommitReviewForm_Shown(object sender, EventArgs e)
      {
         InitialiseData();

         ListRevisions();
      }

      private void ListRevisions()
      {
         int maxRevisions = UiScheduler.UiExecute<int>(() => WorkItems.MaxRevisionsSelected);

         ProgressForm<Tuple<IEnumerable<WorkItem>, IEnumerable<User>, IEnumerable<UserGroup>>>.Execute(
            this,
            string.Format("preparing..."),
            () =>
               {
                  IEnumerable<WorkItem> history = _presenter.GetCommitedWorkItems(maxRevisions);
                  IEnumerable<User> users = _presenter.Users;
                  IEnumerable<UserGroup> groups = _presenter.Groups;
                  return new Tuple<IEnumerable<WorkItem>, IEnumerable<User>, IEnumerable<UserGroup>>(
                     history, users, groups);
               },
            RenderRevisions);
      }

      private void RenderRevisions(
         Tuple<IEnumerable<WorkItem>, IEnumerable<User>, IEnumerable<UserGroup>> data,
         Exception ex)
      {
         if(ex != null)
         {
            Messages.ShowError(ex);
            WorkItems.WorkItems = null;
         }
         else
         {
            WorkItems.WorkItems = data.Item1;
            if(data.Item2 != null || data.Item3 != null) Review.SetData(data.Item2, data.Item3);
         }
      }

      private void PostReview_Click(object sender, EventArgs e)
      {
         var range = _presenter.GetRange(WorkItems.WorkItems);
         if (range != null)
         {
            Review.Fill(_review, _presenter);

            ProgressForm<Review>.Execute(this,
                                         "posting review...",
                                         () =>
                                            {
                                               _presenter.PostReview(range.Item1, range.Item2, _review);
                                               return _review;
                                            },
                                         RenderPostReview);
         }
      }

      private void RenderPostReview(Review r, Exception ex)
      {
         if(ex != null)
         {
            Messages.ShowError(ex);
         }
         else
         {
            _presenter.OpenInBrowser(_review);            
         }
      }

      private void ValidateCanPost()
      {
         var workItems = WorkItems.WorkItems;
         Tuple<int, int> range = _presenter.GetRange(workItems);
         CommandLine.Text = _presenter.GetCommandLine(range);
         PostReview.Enabled = range != null;
      }

      private void SaveDiff_Click(object sender, EventArgs e)
      {
         /*var range = _presenter.GetRange(SelectedWorkItems);
         if (range != null)
         {
            var dlg = new SaveFileDialog();
            dlg.FileName = "my.diff";
            dlg.Filter = "DIFFs (*.diff)|*.diff";
            if (DialogResult.OK == dlg.ShowDialog(this))
            {
               ProgressForm<object>.Execute(this, "saving diff...",
                  () =>
                  {
                     _presenter.SaveDiff(range.Item1, range.Item2, dlg.FileName);
                     return null;
                  }, null);

               
            }
         }*/
      }

      public ILoginPasswordView CreateLoginPasswordView()
      {
         return new RBAuthForm(this);
      }
   }
}
