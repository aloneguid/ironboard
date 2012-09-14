using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi.Model
{
   public class Reviewer
   {
      public Reviewer(string internalName, string name)
      {
         this.InternalName = internalName;
         this.Name = name;
      }

      public string InternalName { get; set; }

      public string Name { get; set; }

      public override string ToString()
      {
         return string.IsNullOrWhiteSpace(Name) ? InternalName : Name;
      }
   }
}
