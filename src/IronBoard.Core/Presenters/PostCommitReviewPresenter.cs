using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using IronBoard.Core.Application;
using IronBoard.Core.Model;
using IronBoard.Core.Views;
using IronBoard.RBWebApi;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{
   public class PostCommitReviewPresenter
   {
      private readonly IPostCommitReviewView _view;
      private readonly RBClient _rb;
      private List<User> _users;
      private List<UserGroup> _groups;
      private readonly SvnRepository _svn;

      public PostCommitReviewPresenter(IPostCommitReviewView view)
      {
         if (view == null) throw new ArgumentNullException("view");
         _view = view;

         if (IbApplication.LoginView == null) IbApplication.LoginView = _view.CreateLoginPasswordView();
         _rb = IbApplication.RBClient;
         _svn = IbApplication.SvnRepository;
      }

      public string SvnRepositoryUri { get { return _svn.RepositoryUri.ToString(); } }

      public IEnumerable<WorkItem> GetCommitedWorkItems(int maxRevisions)
      {
         return _svn.GetCommitedWorkItems(maxRevisions);
      }

      public string ProduceDescription(IEnumerable<WorkItem> selectedItems)
      {
         if (selectedItems != null)
         {
            var b = new StringBuilder();
            foreach (WorkItem wi in selectedItems)
            {
               if (!string.IsNullOrEmpty(wi.Comment))
               {
                  //b.Append("--------- r");
                  //b.Append(wi.ItemId);
                  //b.Append("-----------");
                  //b.AppendLine();

                  b.AppendLine(wi.Comment);

               }
            }
            return b.ToString();
         }
         return null;
      }

      public Tuple<int, int> GetRange(IEnumerable<WorkItem> items)
      {
         int min = int.MaxValue, max = 0;
         if (items != null)
         {
            foreach (WorkItem wi in items)
            {
               int rev = int.Parse(wi.ItemId);
               if (rev < min) min = rev;
               if (rev > max) max = rev;
            }
         }
         return min <= max ? new Tuple<int, int>(min - 1, max) : null;
      }

      public string GetCommandLine(Tuple<int, int> range)
      {
         return range == null
                   ? string.Empty
                   : string.Format("post-review --revision-range={0}:{1}", range.Item1, range.Item2);
      }

      public void PostReview(long fromRev, long toRev, Review review)
      {
         string diff = _svn.GetDiff(fromRev, toRev);

         review.Repository = _rb.GetRepositories().First();
         _rb.Post(review);
         _rb.AttachDiff(review, _svn.RelativeRoot, diff);
      }

      public void SaveDiff(long fromRev, long toRev, string targetFileName)
      {
         string diff = _svn.GetDiff(fromRev, toRev);
         File.WriteAllText(targetFileName, diff, Encoding.UTF8);
      }

      public void OpenInBrowser(Review r)
      {
         Process p = new Process();
         p.StartInfo.UseShellExecute = true;
         p.StartInfo.FileName = string.Format("{0}/r/{1}", _rb.ServerUri, r.Id);
         p.Start();
      }

      public IEnumerable<User> Users
      {
         get { return _users ?? (_users = new List<User>(_rb.GetUsers())); }
      }

      public IEnumerable<UserGroup> Groups
      {
         get { return _groups ?? (_groups = new List<UserGroup>(_rb.GetGroups())); }
      }

      public IEnumerable<User> AsUsers(IEnumerable<string> usernames)
      {
         var users = Users.Where(u => usernames.Any(un => un == u.Username));
         return new List<User>(users);
      }

      public IEnumerable<UserGroup> AsGroups(IEnumerable<string> groupNames)
      {
         var groups = Groups.Where(g => groupNames.Any(gn => gn == g.Name));
         return new List<UserGroup>(groups);
      }
   }
}
