using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{
   class ReviewDetailsPresenter
   {
      private string _lastDiff;

      public ReviewDetailsPresenter(IReviewDetailsView view)
      {
         
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
   }
}
