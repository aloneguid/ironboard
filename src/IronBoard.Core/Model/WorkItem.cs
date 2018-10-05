using System;
using System.Collections.Generic;

namespace IronBoard.Core.Model
{
   [Serializable]
   public class WorkItem
   {
      private readonly HashSet<string> _changedFilePaths = new HashSet<string>();

      public WorkItem(string itemId, string author, string comment, DateTime time)
      {
         ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
         Author = author ?? throw new ArgumentNullException(nameof(author));
         Comment = comment ?? "--No comment--";
         Time = time;
      }

      public string ItemId { get; }

      public string Author { get; }

      public string Comment { get; set; }

      public DateTime Time { get; }

      public ICollection<string> ChangedFilePaths => _changedFilePaths;
   }
}
