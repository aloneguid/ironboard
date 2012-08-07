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
         _presenter.LocalDirectory = new DirectoryInfo("c:\\devel\\pundit");
         ListRevisions();
      }

      private void ListRevisions()
      {
         int maxRevisions = int.Parse((string) MaxRevisions.SelectedItem);

         IEnumerable<WorkItem> history = _presenter.GetCommitedWorkItems(maxRevisions);
         HistoryGrid.Rows.Clear();
         if (history != null)
         {
            foreach (WorkItem wi in history)
            {
               HistoryGrid.Rows.Add(false, wi.ItemId, wi.Author, wi.Time, wi.Comment);
            }
         }
      }

      private List<WorkItem> SelectedWorkItems
      {
         get
         {
            var result = new List<WorkItem>();
            foreach (DataGridViewRow row in HistoryGrid.Rows)
            {
               var isSelected = (DataGridViewCheckBoxCell)row.Cells[0];
               bool isSelectedBool = (bool) isSelected.Value;
               if (isSelectedBool)
               {
                  var revision = (DataGridViewTextBoxCell) row.Cells[1];
                  var author = (DataGridViewTextBoxCell) row.Cells[2];
                  var time = (DataGridViewTextBoxCell)row.Cells[3];
                  var comment = (DataGridViewTextBoxCell) row.Cells[4];
                  result.Add(new WorkItem((string)revision.Value,
                     (string)author.Value,
                     (string)comment.Value,
                     (DateTime)time.Value));
               }
            }
            return result;
         }
      }

      private void HistoryGrid_CellClick(object sender, DataGridViewCellEventArgs e)
      {
         
      }

      private void HistoryGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
      {
         UpdateDescription();
      }

      private void UpdateDescription()
      {
         string txt = _presenter.ProduceDescription(SelectedWorkItems);
         if (txt != null)
         {
            Description.Text = txt;
         }
      }

      private void HistoryGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
      {
         UpdateDescription();
      }

      private void PostReview_Click(object sender, EventArgs e)
      {

      }
   }
}
