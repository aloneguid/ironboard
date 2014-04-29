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
         if (itemId == null) throw new ArgumentNullException("itemId");
         if (author == null) throw new ArgumentNullException("author");
         if (comment == null) throw new ArgumentNullException("comment");

         ItemId = itemId;
         Author = author;
         Comment = comment;
         Time = time;
      }

      public string ItemId { get; private set; }

      public string Author { get; private set; }

      public string Comment { get; set; }

      public DateTime Time { get; private set; }

      public ICollection<string> ChangedFilePaths
      {
         get { return _changedFilePaths; }
      } 
   }
}
