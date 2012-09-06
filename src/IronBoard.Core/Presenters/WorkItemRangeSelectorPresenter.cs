using IronBoard.Core.Model;
using IronBoard.Core.Views;

namespace IronBoard.Core.Presenters
{
   public class WorkItemRangeSelectorPresenter
   {
      public WorkItemRangeSelectorPresenter(IWorkItemRangeSelectorView view)
      {
         
      }

      public string ToListString(WorkItem i)
      {
         return string.Format("{0}: {1}@{2}| {3}",
                              i.ItemId, i.Author, i.Time, i.Comment);
      }
   }
}
