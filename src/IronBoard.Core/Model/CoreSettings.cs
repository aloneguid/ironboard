using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.Core.Model
{
   public class CoreSettings
   {
      public string AuthCookie { get; set; }

      public int MaxRevisions { get; set; }

      public string TargetUsers { get; set; }

      public string TargetGroups { get; set; }
   }
}
