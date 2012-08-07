using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using IronBoard.Core.Model;
using SharpSvn;

namespace IronBoard.Core.Presenters
{
   class PostCommitReviewPresenter
   {
      public DirectoryInfo LocalDirectory { get; set; }

      public IEnumerable<WorkItem> GetCommitedWorkItems(int maxRevisions)
      {
         var args = new SvnLogArgs {Limit = maxRevisions};

         using (var client = new SvnClient())
         {
            Collection<SvnLogEventArgs> entries;
            client.GetLog(LocalDirectory.FullName, args, out entries);

            if(entries != null && entries.Count > 0)
            {
               return entries.Select(e => new WorkItem(
                                             e.Revision.ToString(CultureInfo.InvariantCulture),
                                             e.Author,
                                             e.LogMessage,
                                             e.Time));
            }
         }

         return null;
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
                  b.Append("--------- r");
                  b.Append(wi.ItemId);
                  b.AppendLine("-----------");

                  b.AppendLine(wi.Comment);

               }
            }
            return b.ToString();
         }
         return null;
      }
   }
}
