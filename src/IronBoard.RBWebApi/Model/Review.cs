using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi.Model
{
   public class Review
   {
      public Review()
      {
         
      }

      public long Id { get; set; }

      public Repository Repository { get; set; }

      public string Subject { get; set; }

      public string Description { get; set; }

      public string TestingDone { get; set; }

      public bool IsDraft { get; set; }
   }
}
