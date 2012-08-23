using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using IronBoard.Core.Model;
using IronBoard.Core.Views;
using IronBoard.Core.WinForms;
using IronBoard.RBWebApi;
using IronBoard.RBWebApi.Model;
using SharpSvn;

namespace IronBoard.Core.Presenters
{
   public class PostCommitReviewPresenter
   {
      private readonly IPostCommitReviewView _view;
      private SvnClient _svn;
      private SvnUriTarget _root;
      private string _relativeRoot;
      private RBClient _rb;
      private readonly string _oldCookie;
      private List<User> _users;
      private List<UserGroup> _groups; 

      public event Action<string> AuthCookieChanged;

      public PostCommitReviewPresenter(IPostCommitReviewView view, string authCookie)
      {
         if (view == null) throw new ArgumentNullException("view");
         _view = view;
         _oldCookie = authCookie;
      }

      public void Initialise(string workingCopyPath)
      {
         _svn = new SvnClient();
         SvnInfoEventArgs args;
         _svn.GetInfo(new SvnPathTarget(workingCopyPath), out args);
         _root = new SvnUriTarget(args.Uri);
         string root = args.Uri.ToString();
         string repoRoot = args.RepositoryRoot.ToString();
         _relativeRoot = root.Substring(repoRoot.Length - 1);
         _rb = new RBClient(args.Uri.ToString(), workingCopyPath, _oldCookie);
         _rb.AuthenticationRequired += OnAuthenticationRequired;
         _rb.AuthCookieChanged += OnAuthCookieChanged;
      }

      void OnAuthCookieChanged(string cookie)
      {
         if (AuthCookieChanged != null) AuthCookieChanged(cookie);
      }

      void OnAuthenticationRequired(NetworkCredential cred)
      {
         NetworkCredential newCred = null;

         UiScheduler.UiExecute(() =>
            {
               ILoginPasswordView view = _view.CreateLoginPasswordView();
               newCred = view.CollectCredential();
            }, true);

         if(newCred != null)
         {
            cred.UserName = newCred.UserName;
            cred.Password = newCred.Password;
         }
      }

      public string SvnRepositoryUri { get { return _root.Uri.ToString(); } }

      public IEnumerable<WorkItem> GetCommitedWorkItems(int maxRevisions)
      {
         var args = new SvnLogArgs {Limit = maxRevisions};

         Collection<SvnLogEventArgs> entries;
         _svn.GetLog(_root.Uri, args, out entries);

         if (entries != null && entries.Count > 0)
         {
            return entries.Select(e => new WorkItem(
                                          e.Revision.ToString(CultureInfo.InvariantCulture),
                                          e.Author,
                                          e.LogMessage,
                                          e.Time));
         }

         return null;
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

      public string ToListString(WorkItem i)
      {
         return string.Format("{0}: {1}@{2}| {3}",
                              i.ItemId, i.Author, i.Time, i.Comment);
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

      private string GetDiff(long fromRev, long toRev)
      {
         string diffText;
         using (var ms = new MemoryStream())
         {
            _svn.Diff(
               _root,
               new SvnRevisionRange(fromRev, toRev),
               ms);

            ms.Position = 0;
            diffText = Encoding.UTF8.GetString(ms.ToArray());
         }

         return diffText;
      }

      public void PostReview(long fromRev, long toRev, Review review)
      {
         string diff = GetDiff(fromRev, toRev);

         review.Repository = _rb.GetRepositories().First();
         _rb.Post(review);
         _rb.AttachDiff(review, _relativeRoot, diff);
      }

      public void SaveDiff(long fromRev, long toRev, string targetFileName)
      {
         string diff = GetDiff(fromRev, toRev);
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
