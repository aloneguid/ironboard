using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronBoard.Core.Application;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{

   public delegate string GetDiff();

   class ReviewDetailsPresenter
   {
      private readonly IReviewDetailsView _view;
      private readonly GetDiff _getDiff;
      private string _lastDiff;

      public ReviewDetailsPresenter(IReviewDetailsView view, GetDiff getDiff)
      {
         _view = view;
         _getDiff = getDiff;
      }

      public string GenerateDiff()
      {
         if (_lastDiff == null)
         {
             _lastDiff = _getDiff();
         }

         return _lastDiff;
      }

      public string LastDiff
      {
         get { return _lastDiff; }
      }

      public void OpenLastDiff()
      {
         if (_lastDiff == null) return;

         string targetPath = Path.Combine(Path.GetTempPath(), "ironboard-last.diff");
         File.WriteAllText(targetPath, _lastDiff, Encoding.UTF8);
         IbApplication.OpenFile(targetPath);
      }

      private static IEnumerable<User> _users;
      private static IEnumerable<UserGroup> _groups;
      public IEnumerable<User> Users
      {
         get { return _users ?? (_users = IbApplication.RbClient.GetUsers()); }
      }
      public IEnumerable<UserGroup> Groups
      {
         get { return _groups ?? (_groups = IbApplication.RbClient.GetGroups()); }
      }

      public void PostReview(Review r)
      {
         Task.Factory.StartNew(() =>
            {
               Exception ex = null;
               try
               {
                  _view.UpdatePostStatus(Strings.PostProgress_DetectRepository);
                  r.Repository = IbApplication.RbClient
                     .GetRepositories()
                     .FirstOrDefault(x => string.Equals(SvnRepository.TrimRepositoryUrl(x.Path),
                        SvnRepository.TrimRepositoryUrl(IbApplication.SvnRepository.RelativeRepositoryUri),
                        StringComparison.InvariantCultureIgnoreCase));

                  if (r.Repository == null)
                  {
                     string allRepos = string.Join("; ", IbApplication.RbClient.GetRepositories().Select(ir => ir.Path));

                     throw new ApplicationException(
                        string.Format("cannot find a valid SVN repository, was looking for [{0}], relative root: [{1}], path: [{2}], your server has: [{3}]",
                           IbApplication.SvnRepository.RepositoryUri.AbsoluteUri,
                           IbApplication.SvnRepository.RelativeRoot,
                           IbApplication.SvnRepository.RelativeRepositoryUri,
                           allRepos));
                  }

                  _view.UpdatePostStatus(Strings.PostProgress_MainTicket);
                  IbApplication.RbClient.Post(r);

                  _view.UpdatePostStatus(Strings.PostProgress_Diff);
                  IbApplication.RbClient.AttachDiff(r, IbApplication.SvnRepository.RelativeRoot, _lastDiff);
                  IbApplication.RbClient.MakePublic(r);
               }
               catch (Exception ex1)
               {
                  ex = ex1;
               }
               _view.UpdatePostFinish(ex);
            });
      }
   }
}
