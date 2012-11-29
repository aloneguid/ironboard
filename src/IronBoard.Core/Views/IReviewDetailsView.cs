using System;

namespace IronBoard.Core.Views
{
   public interface IReviewDetailsView
   {
      void UpdatePostStatus(string status);

      void UpdatePostFinish(Exception error);
   }
}
