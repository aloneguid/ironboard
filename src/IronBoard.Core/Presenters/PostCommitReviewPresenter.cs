using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using IronBoard.Core.Model;
using IronBoard.Core.Views;
using IronBoard.RBWebApi;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{
   public class PostCommitReviewPresenter
   {
      private readonly IPostCommitReviewView _view;
      private readonly IRbClient _rb;
      private List<User> _users;
      private List<UserGroup> _groups;
      private readonly ICodeRepository _scm;

      public PostCommitReviewPresenter(IPostCommitReviewView view)
      {
         if (view == null) throw new ArgumentNullException("view");
         _view = view;

         if (IbApplication.LoginView == null) IbApplication.LoginView = _view.CreateLoginPasswordView();
         _rb = IbApplication.RbClient;
         _scm = IbApplication.CodeRepository;
      }

      public string RemoteRepositoryUri { get { return _scm.RemoteRepositoryUri.ToString(); } }

      public IEnumerable<WorkItem> GetCommitedWorkItems(int maxRevisions)
      {
         return _scm.GetHistory(maxRevisions);
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

      public void PostReview(RevisionRange range, Review review)
      {
         string diff = _scm.GetDiff(range);
         string repositoryPath = _scm.RemoteRepositoryUri.AbsoluteUri.Replace(_scm.RelativeRoot, "");
         review.Repository = _rb.GetRepositories().First( x => string.Equals(x.Path, repositoryPath, StringComparison.InvariantCultureIgnoreCase));
         _rb.Post(review);
         _rb.AttachDiff(review, _scm.RelativeRoot, diff);
      }

      public void SaveDiff(RevisionRange range, string targetFileName)
      {
         string diff = _scm.GetDiff(range);
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
         var users = Users.Where(u => usernames.Any(un => un == u.InternalName));
         return new List<User>(users);
      }

      public IEnumerable<UserGroup> AsGroups(IEnumerable<string> groupNames)
      {
         var groups = Groups.Where(g => groupNames.Any(gn => gn == g.Name));
         return new List<UserGroup>(groups);
      }
   }
}
