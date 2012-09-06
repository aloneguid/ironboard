using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.Core.Model
{
   [Serializable]
   public class WorkItem
   {
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

      public string Comment { get; private set; }

      public DateTime Time { get; private set; }
   }
}
