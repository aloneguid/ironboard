using System;
using System.Collections.Generic;
using IronBoard.Core.Model;

namespace IronBoard.Core.Application
{
   static class GenericRepository
   {
      public static RevisionRange GetRange(IEnumerable<WorkItem> items)
      {
         DateTime minDate = DateTime.MaxValue;
         DateTime maxDate = DateTime.MinValue;
         string min = null;
         string max = null;

         if (items != null)
         {
            foreach (WorkItem wi in items)
            {
               string rev = wi.ItemId;

               if (min == null)
               {
                  min = rev;
                  minDate = wi.Time;
               }
               else if (wi.Time < minDate)
               {
                  min = rev;
                  minDate = wi.Time;
               }

               if (max == null)
               {
                  max = rev;
                  maxDate = wi.Time;
               }
               else if (wi.Time > maxDate)
               {
                  max = rev;
                  maxDate = wi.Time;
               }
            }
         }
         return minDate <= maxDate ? new RevisionRange(min, max) : null;
      }

   }
}
