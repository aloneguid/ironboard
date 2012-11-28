using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.RBWebApi.Model
{
   public class ReviewLinks
   {
      public Uri Diffs { get; set; }

      public Uri Update { get; set; }

      public Uri Draft { get; set; }

      public Uri Self { get; set; }
   }

   public class Review
   {
      private readonly List<User> _targetPeople = new List<User>(); 
      private readonly List<UserGroup> _targetGroups = new List<UserGroup>(); 

      public Review()
      {
         
      }

      public Review(long id, string subject, string description, string testingDone, string bugsClosed, DateTime lastUpdated)
      {
         Id = id;
         Subject = subject;
         Description = description;
         TestingDone = testingDone;
         BugsClosed = bugsClosed;
         LastUpdated = lastUpdated;
      }

      public long Id { get; set; }

      public Repository Repository { get; set; }

      public string Subject { get; set; }

      public string Description { get; set; }

      public string TestingDone { get; set; }

      public string BugsClosed { get; set; }

      public bool IsDraft { get; set; }

      public ReviewLinks Links { get; set; }

      public User Submitter { get; set; } 

      public ICollection<User> TargetUsers { get { return _targetPeople;  } }

      public ICollection<UserGroup> TargetGroups { get { return _targetGroups; } }

      public DateTime LastUpdated { get; set; }

      public string Status { get; set; }
   }
}
