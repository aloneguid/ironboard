using System;
using IronBoard.Core.Model;
using LibGit2Sharp;

namespace IronBoard.Core.Application
{
   public class GitRepository : ICodeRepository
   {
      private readonly string _workingCopyPath;
      private Repository _git;

      public GitRepository(string workingCopyPath)
      {
         if (workingCopyPath == null) throw new ArgumentNullException("workingCopyPath");
         _workingCopyPath = workingCopyPath;

         _git = new Repository(workingCopyPath);
      }

      public string Branch
      {
         get
         {
            return null;
         }
      }

      public void Dispose()
      {
         _git.Dispose();  
      }
   }
}
