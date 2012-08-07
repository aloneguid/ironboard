using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;

namespace IronBoard.Core.WinForms
{
   public partial class PostCommitReviewForm : Form
   {
      private readonly PostCommitReviewPresenter _presenter = new PostCommitReviewPresenter();

      public PostCommitReviewForm()
      {
         InitializeComponent();

         MaxRevisions.SelectedIndex = 0;
         CommandLine.Text = string.Empty;
         _presenter.Initialise("c:\\devel\\pundit");
         Shown += PostCommitReviewForm_Shown;
      }

      void PostCommitReviewForm_Shown(object sender, EventArgs e)
      {
         ListRevisions();
      }

      private void ListRevisions()
      {
         int maxRevisions = int.Parse((string) MaxRevisions.SelectedItem);

         IEnumerable<WorkItem> history = _presenter.GetCommitedWorkItems(maxRevisions);;

         Revisions.Items.Clear();
         if (history != null)
         {
            foreach (WorkItem wi in history)
            {
               Revisions.Items.Add(
                  new DisplayItem<WorkItem>(_presenter.ToListString(wi), wi));
            }
         }
      }

      private List<WorkItem> SelectedWorkItems
      {
         get
         {
            //this can only work on continuos selection
            var result = new List<WorkItem>();
            bool started = false;
            for (int i = 0; i < Revisions.Items.Count; i++)
            {
               var di = Revisions.Items[i] as DisplayItem<WorkItem>;
               if(di != null)
               {
                  bool isChecked = Revisions.GetItemChecked(i);
                  if(!isChecked)
                  {
                     if (started) break;
                  }
                  else
                  {
                     result.Add(di.Data);
                     started = true;
                  }
               }
            }
            return result;
         }
      }

      private void UpdateRevisionsChanged()
      {
         var selection = SelectedWorkItems;

         string txt = _presenter.ProduceDescription(selection);
         if (txt != null)
         {
            Description.Text = txt;
         }

         Validate();
      }

      private void PostReview_Click(object sender, EventArgs e)
      {
         var range = _presenter.GetRange(SelectedWorkItems);
         if (range != null)
         {
            _presenter.PostReview(range.Item1, range.Item2,
               Summary.Text, Description.Text, Testing.Text);
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

      private void Validate()
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
               _presenter.SaveDiff(range.Item1, range.Item2, dlg.FileName);
            }
         }
      }
   }
}
