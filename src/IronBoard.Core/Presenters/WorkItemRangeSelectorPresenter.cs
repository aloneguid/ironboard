using System.Collections.Generic;
using System.Linq;
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

      public IEnumerable<WorkItem> GetCurrentWorkItems(int maxItems)
      {
         IEnumerable<WorkItem> items = null;
         if(IbApplication.SvnRepository != null)
         {
            items = IbApplication.SvnRepository.GetCommitedWorkItems(maxItems);
            if (items != null) items = items.OrderBy(i => -int.Parse(i.ItemId)); //order by revision number desc
         }
         return items;
      }
   }
}
