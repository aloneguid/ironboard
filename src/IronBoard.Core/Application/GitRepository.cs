using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
         return Exec("diff {0}", "origin/master");
      }

      public override string GetDiff(string @from, string to)
      {
         throw new NotImplementedException();
      }

      public override IEnumerable<WorkItem> GetHistory(int maxEntries)
      {
         string log = Exec("log -{0}", maxEntries);

         return ParseLog(log);
      }

      public override void Dispose()
      {
      }

      private IEnumerable<WorkItem> ParseLog(string log)
      {
         var result = new List<WorkItem>();

         string itemId = null;
         string author = null;
         string comment = null;
         DateTime date = DateTime.UtcNow;

         foreach (string line in log.Split('\n'))
         {
            if (line.StartsWith("commit "))
            {
               if (itemId != null)
               {
                  var item = new WorkItem(itemId, author, comment, date);
                  result.Add(item);
                  author = comment = null;
                  date = DateTime.UtcNow;
               }

               itemId = line.Substring(7).Trim();
            }
            else if (line.StartsWith("author:", StringComparison.InvariantCultureIgnoreCase))
            {
               author = line.Substring(7).Trim();
            }
            else if (line.StartsWith("date:", StringComparison.InvariantCultureIgnoreCase))
            {
               //date example: Mon Apr 28 11:55:45 2014 +0100
               string sd = line.Substring(5).Trim();
               date = DateTime.Parse(sd);
            }
            else
            {
               string s = line.Trim();
               if (!string.IsNullOrEmpty(s))
               {
                  if (comment == null)
                  {
                     comment = s;
                  }
                  else
                  {
                     comment += Environment.NewLine;
                     comment += s;
                  }
               }
            }
         }

         if (itemId != null)
         {
            result.Add(new WorkItem(itemId, author, comment, date));
         }

         return result;
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
