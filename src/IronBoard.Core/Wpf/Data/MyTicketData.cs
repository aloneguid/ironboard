using System.Linq;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Wpf.Data
{
   public class MyTicketData
   {
      public Review R { get; set; }

      public MyTicketData(Review review)
      {
         R = review;

         To = string.Empty;
         if (review.TargetGroups.Count > 0)
         {
            To += string.Join(", ", review.TargetGroups.Select(g => g.ToString()));
         }
         if (review.TargetUsers.Count > 0)
         {
            if (To.Length > 0) To += "; ";
            To += string.Join(", ", review.TargetUsers.Select(u => u.ToString()));
         }
      }

      public string To { get; set; }
   }
}
