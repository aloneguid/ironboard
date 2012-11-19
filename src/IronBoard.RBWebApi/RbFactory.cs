using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi
{
   public static class RbFactory
   {
      public static IRbClient CreateHttpClient(string projectRootFolder, string authCookie)
      {
         return new RbHttpClient(projectRootFolder, authCookie);
      }

      public static IRbClient CreateMockedClient()
      {
         return new RbMockedClient();
      }
   }
}
