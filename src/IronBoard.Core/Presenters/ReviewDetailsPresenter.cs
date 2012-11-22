using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using IronBoard.Core.Views;

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

         Process p = new Process();
         p.StartInfo.UseShellExecute = true;
         p.StartInfo.FileName = targetPath;
         p.Start();
      }
   }
}
