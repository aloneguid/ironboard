using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{
   class ReviewDetailsPresenter
   {
      private readonly IReviewDetailsView _view;
      private string _lastDiff;

      public ReviewDetailsPresenter(IReviewDetailsView view)
      {
         _view = view;
      }

      public string GenerateDiff(long fromRev, long toRev)
      {
         if (_lastDiff == null)
         {
            _lastDiff = IbApplication.SvnRepository.GetDiff(fromRev, toRev);
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
                  string repositoryPath = IbApplication.SvnRepository.RepositoryUri.AbsoluteUri.Replace(IbApplication.SvnRepository.RelativeRoot, "");
                  r.Repository = IbApplication.RbClient.GetRepositories().First(x => string.Equals(x.Path, repositoryPath, StringComparison.InvariantCultureIgnoreCase));

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
