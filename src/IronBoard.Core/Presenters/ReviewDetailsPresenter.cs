using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.Core.Views;

namespace IronBoard.Core.Presenters
{
   class ReviewDetailsPresenter
   {
      private string _lastDiff;

      public ReviewDetailsPresenter(IReviewDetailsView view)
      {
         
      }

      public string GenerateDiff(long fromRev, long toRev)
      {
         if (_lastDiff == null)
         {
            _lastDiff = IbApplication.SvnRepository.GetDiff(fromRev, toRev);
         }

         return _lastDiff;
      }
   }
}
