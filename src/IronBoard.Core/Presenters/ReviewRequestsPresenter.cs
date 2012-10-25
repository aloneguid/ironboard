using System.Collections.Generic;
using System.Linq;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{
   class ReviewRequestsPresenter
   {
      public ReviewRequestsPresenter(IReviewRequestsView view)
      {
         
      }

      public object[] FormatGridRow(Review r)
      {
         return new object[]
            {
               r.Id,
               r.Submitter,
               r.LastUpdated,  //updated
               r.Status,  //status
               Format(r.TargetGroups),
               Format(r.TargetUsers),
               r.Subject
            };
      }

      public string Format(IEnumerable<Reviewer> rs)
      {
         if (rs == null) return string.Empty;
         return string.Join("; ", rs.Select(r => r.Name));
      }
   }
}
