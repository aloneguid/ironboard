using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.Core.Model.IssueTracker
{
   public class TrackerIssue
   {
      public TrackerIssue(string id)
      {
         this.Id = id;
      }

      public string Id { get; private set; }

      public string ProjectId { get; private set; }

      public string[] FixVersions { get; set; }

      public string Reporter { get; set; }

      public string Assignee { get; set; }

      public string Subject { get; set; }

      public string Description { get; set; }

      public DateTime? Created { get; set; }

      public DateTime? Updated { get; set; }

      public string Resolution { get; set; }

      public string Status { get; set; }

      public Dictionary<string, string> CustomFields { get; set; } 

      public override string ToString()
      {
         return string.Format("{0}|{1}: {2}", Id, Status, Subject);
      }
   }
}
