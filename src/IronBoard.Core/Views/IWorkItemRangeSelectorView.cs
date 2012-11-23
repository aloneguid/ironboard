using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.Core.Model;

namespace IronBoard.Core.Views
{
   public interface IWorkItemRangeSelectorView
   {
      void UpdateList(Exception ex, IEnumerable<WorkItem> items);
   }
}
