using System;
using System.Collections.Generic;
using System.Linq;
using IronBoard.Core.Model;

namespace IronBoard.Core.Application
{
   public class GitRepository : CommandLineRepository
   {
      public GitRepository(string workingCopyPath) : base(workingCopyPath, "git")
      {
      }

      public override string ClientVersion
      {
         get { return Exec("--version"); }
      }

      public override string Branch
      {
         get
         {
            string branchInfo = Exec("branch");

            return ExtractCurrentBranch(branchInfo);
         }
      }

      public override string RelativeRoot { get { throw new NotImplementedException(); } }

      public override Uri RemoteRepositoryUri
      {
         get
         {
            string url = Exec("config --get remote.origin.url");

            return new Uri(url);
         }
      }

      public override string RelativeRepositoryUri { get { throw new NotImplementedException(); } }

      public override string GetLocalDiff()
      {
         return Exec("diff");
      }

      public override string GetDiff(string @from, string to)
      {
         throw new NotImplementedException();
      }

      public override IEnumerable<WorkItem> GetHistory(int maxEntries)
      {
         throw new NotImplementedException();
      }

      public override void Dispose()
      {
      }

      private string ExtractCurrentBranch(string branchInfo)
      {
         string current = Split(branchInfo).FirstOrDefault(b => b.StartsWith("* "));

         return current == null ? null : current.Substring(2).Trim();
      }

      private string[] Split(string output)
      {
         return output.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
      }
   
   }
}
