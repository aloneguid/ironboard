using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IronBoard.Core.Model;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;

namespace IronBoard.Core.WinForms
{
   public partial class WorkItemRangeSelector : UserControl, IWorkItemRangeSelectorView
   {
      public event Action<List<WorkItem>> SelectedWorkItemsChanged;
      public event Action<object> WorkItemsInvalidated;

      private readonly WorkItemRangeSelectorPresenter _presenter;

      public WorkItemRangeSelector()
      {
         InitializeComponent();
         MaxRevisions.SelectedIndex = 0;
         CommandLine.Text = string.Empty;

         _presenter = new WorkItemRangeSelectorPresenter(this);
      }

      public int MaxRevisionsSelected
      {
         get { return int.Parse(MaxRevisions.SelectedItem.ToString()); }
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

      private void UpdateRevisionsChanged()
      {
         List<WorkItem> items = SelectedWorkItems; //always call, updates UI
         if(SelectedWorkItemsChanged != null)
         {
            SelectedWorkItemsChanged(items);
         }
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
               if (di != null)
               {
                  bool isChecked = Revisions.GetItemChecked(i);
                  if (!isChecked)
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

      public IEnumerable<WorkItem> WorkItems
      {
         set
         {
            Revisions.Items.Clear();

            if (value != null)
            {
               foreach (WorkItem wi in value)
               {
                  Revisions.Items.Add(
                     new DisplayItem<WorkItem>(_presenter.ToListString(wi), wi));
               }
            }
         }
         get { return SelectedWorkItems; }
      }

      private void Refresh_Click(object sender, EventArgs e)
      {
         if (WorkItemsInvalidated != null) WorkItemsInvalidated(null);
      }

      public void UpdateList(Exception ex, IEnumerable<WorkItem> items)
      {
         throw new NotImplementedException();
      }
   }
}
