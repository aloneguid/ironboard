using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.WinForms
{
   public partial class PostCommitReviewForm : Form, IPostCommitReviewView
   {
      private readonly PostCommitReviewPresenter _presenter;
      private readonly Review _review = new Review();

      public event Action<string> AuthCookieChanged;

      public PostCommitReviewForm(string solutionPath, string authCookie)
      {
         InitializeComponent();

         _presenter = new PostCommitReviewPresenter(this, authCookie);
         _presenter.AuthCookieChanged += PresenterAuthCookieChanged;

         UiScheduler.InitializeUiContext();

         MaxRevisions.SelectedIndex = 0;
         CommandLine.Text = string.Empty;
         _presenter.Initialise(solutionPath);
         SvnUri.Text = _presenter.SvnRepositoryUri;
         Progress.Text = "Idle";
         Shown += PostCommitReviewForm_Shown;
         Closed += PostCommitReviewForm_Closed;
      }

      void PresenterAuthCookieChanged(string cookie)
      {
         if (AuthCookieChanged != null) AuthCookieChanged(cookie);
      }

      void PostCommitReviewForm_Shown(object sender, EventArgs e)
      {
         ListRevisions();
      }

      void PostCommitReviewForm_Closed(object sender, EventArgs e)
      {
         _presenter.AuthCookieChanged -= PresenterAuthCookieChanged;
      }

      private void ListRevisions()
      {
         int maxRevisions = int.Parse((string) MaxRevisions.SelectedItem);

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
         Revisions.Items.Clear();

         if(ex != null)
         {
            Messages.ShowError(ex);
         }
         else
         {
            if (data.Item1 != null)
            {
               foreach (WorkItem wi in data.Item1)
               {
                  Revisions.Items.Add(
                     new DisplayItem<WorkItem>(_presenter.ToListString(wi), wi));
               }
            }
            if(data.Item2 != null || data.Item3 != null) Review.SetData(data.Item2, data.Item3);
         }

         UpdateRevisionsChanged();
      }

      private List<WorkItem> SelectedWorkItems
      {
         get
         {
            //this can only work on continuos selection
            var result = new List<WorkItem>();
            bool started = false;
            bool broken = false;
            bool warn = false;
            for (int i = 0; i < Revisions.Items.Count; i++)
            {
               var di = Revisions.Items[i] as DisplayItem<WorkItem>;
               if(di != null)
               {
                  bool isChecked = Revisions.GetItemChecked(i);
                  if(!isChecked)
                  {
                     if (started) broken = true;
                  }
                  else
                  {
                     if (!broken)
                     {
                        result.Add(di.Data);
                        started = true;
                     }
                     else
                     {
                        warn = true;
                        break;
                     }
                  }
               }
            }

            RevisionsWarning.Visible = warn;

            return result;
         }
      }

      private void UpdateRevisionsChanged()
      {
         var selection = SelectedWorkItems;

         string txt = _presenter.ProduceDescription(selection);
         if (txt != null)
         {
            _review.Description = txt;
            Review.Display(_review);
         }

         ValidateCanPost();
      }

      private void PostReview_Click(object sender, EventArgs e)
      {
         var range = _presenter.GetRange(SelectedWorkItems);
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

      private void Refresh_Click(object sender, EventArgs e)
      {
         ListRevisions();
      }

      private void Revisions_ItemCheck(object sender, ItemCheckEventArgs e)
      {
         //doesn't update last checked item here,
         //what a fuckhead company could implement this? :)
         UpdateRevisionsChanged();
      }

      private void Revisions_MouseUp(object sender, MouseEventArgs e)
      {
         //as a workaround for ItemCheck do it again here
         UpdateRevisionsChanged();
      }

      private void ValidateCanPost()
      {
         var workItems = SelectedWorkItems;
         Tuple<int, int> range = _presenter.GetRange(workItems);

         if(range == null)
         {
            CommandLine.Text = null;
            PostReview.Enabled = false;
            SaveDiff.Enabled = false;
         }
         else
         {
            CommandLine.Text = string.Format("r{0}:{1}", range.Item1, range.Item2);
            PostReview.Enabled = true;
            SaveDiff.Enabled = true;
         }
      }

      private void SaveDiff_Click(object sender, EventArgs e)
      {
         var range = _presenter.GetRange(SelectedWorkItems);
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
         }
      }

      public ILoginPasswordView CreateLoginPasswordView()
      {
         return new RBAuthForm(this);
      }
   }
}
