using System;
using System.IO;
using IronBoard.Core.Model;

namespace IronBoard.Core.Application
{
   public class ScmProviderDetector
   {
      private readonly string _workingCopyPath;

      public ScmProviderDetector(string workingCopyPath)
      {
         if (workingCopyPath == null) throw new ArgumentNullException("workingCopyPath");
         if (!Directory.Exists(workingCopyPath)) throw new DirectoryNotFoundException("working copy not found at " + workingCopyPath);
         _workingCopyPath = workingCopyPath;
      }

      public ScmProvider DetectProvider()
      {
         var di = new DirectoryInfo(_workingCopyPath);

         var provider = ScmProvider.None;
         while (di != null)
         {
            if (IsUnderSourceControl(di, out provider, out di)) return provider;
         }

         return provider;
      }

      private bool IsUnderSourceControl(DirectoryInfo directory, out ScmProvider scmProvider, out DirectoryInfo parentDirectory)
      {
         if (directory.GetDirectories("*.git", SearchOption.TopDirectoryOnly).Length > 0)
         {
            scmProvider = ScmProvider.Git;
            parentDirectory = null;
            return true;
         }

         if (directory.GetDirectories("*.svn", SearchOption.TopDirectoryOnly).Length > 0)
         {
            scmProvider = ScmProvider.Svn;
            parentDirectory = null;
            return true;
         }

         scmProvider = ScmProvider.None;
         parentDirectory = directory.Parent;
         return false;
      }
   }
}
