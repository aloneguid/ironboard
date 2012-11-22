using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

      public Tuple<int, int> GetRange(IEnumerable<WorkItem> items)
      {
         int min = int.MaxValue, max = 0;
         if (items != null)
         {
            foreach (WorkItem wi in items)
            {
               int rev = int.Parse(wi.ItemId);
               if (rev < min) min = rev;
               if (rev > max) max = rev;
            }
         }
         return min <= max ? new Tuple<int, int>(min - 1, max) : null;
      }

      public Tuple<int, int> GetRange(IEnumerable allItems, IEnumerable selectedItems)
      {
         bool skipped;
         return GetRange(SelectContinuousItems(allItems, selectedItems, out skipped));
      }

      public IEnumerable<WorkItem> SelectContinuousItems(IEnumerable allItems, IEnumerable selectedItems, out bool skiped)
      {
         return SelectContinuousItems(allItems.Cast<WorkItem>().ToList(), selectedItems.Cast<WorkItem>().ToList(), out skiped);
      }

      public IEnumerable<WorkItem> SelectContinuousItems(IEnumerable<WorkItem> allItems, IEnumerable<WorkItem> selectedItems, out bool skipped)
      {
         skipped = false;
         var result = new List<WorkItem>();
         var itemToPosition = new Dictionary<WorkItem, int>();
         int t = 0;
         foreach (WorkItem wi in allItems)
         {
            itemToPosition[wi] = t++;
         }
         var selectedSorted = selectedItems.OrderBy(i => itemToPosition[i]);

         int last = -1;
         foreach (WorkItem wi in selectedSorted)
         {
            int pos = itemToPosition[wi];
            if (last == -1)
            {
               last = pos;
               result.Add(wi);
            }
            else if (pos == ++last)
            {
               result.Add(wi);
            }
            else
            {
               skipped = true;
               break;
            }
         }

         return result;
      }

      public string GetCommandLine(Tuple<int, int> range)
      {
         return range == null
                   ? string.Empty
                   : string.Format("post-review --revision-range={0}:{1}", range.Item1, range.Item2);
      }

      public string ProduceDescription(IEnumerable<WorkItem> selectedItems)
      {
         if (selectedItems != null)
         {
            var b = new StringBuilder();
            foreach (WorkItem wi in selectedItems)
            {
               if (!string.IsNullOrEmpty(wi.Comment))
               {
                  b.AppendLine(wi.Comment);
               }
            }
            return b.ToString();
         }
         return null;
      }
   }
}
