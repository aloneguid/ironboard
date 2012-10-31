using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.Core.Model;

namespace IronBoard.Core.Wpf.Data
{
   class WorkItemData
   {
      public bool IsChecked { get; set; }

      public WorkItem WorkItem { get; set; }

      public WorkItemData(WorkItem i)
      {
         this.WorkItem = i;
      }
   }
}
