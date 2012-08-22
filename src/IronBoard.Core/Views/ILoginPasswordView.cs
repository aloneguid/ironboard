using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IronBoard.Core.Views
{
   public interface ILoginPasswordView
   {
      NetworkCredential CollectCredential();
   }
}
