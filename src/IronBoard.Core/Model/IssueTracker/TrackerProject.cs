using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.Core.Model.IssueTracker
{
   public class TrackerProject
   {
      public TrackerProject(string id, string name)
      {
         this.Id = id;
         this.Name = name;
      }

      public string Id { get; private set; }

      public string Name { get; private set; }
   }
}
