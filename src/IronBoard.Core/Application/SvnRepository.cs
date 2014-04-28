using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using IronBoard.Core.Model;
using SharpSvn;

namespace IronBoard.Core.Application
{
   public class SvnRepository : ICodeRepository
   {
      private readonly string _workingCopyPath;
      private SvnClient _svn;
      private SvnUriTarget _root;
      private string _relativeRoot;
      private Uri _repositoryUri;

      public SvnRepository(string workingCopyPath)
      {
         if (workingCopyPath == null) throw new ArgumentNullException("workingCopyPath");
         _workingCopyPath = workingCopyPath;

         Initialize();
      }

      public Uri RepositoryUri { get { return _repositoryUri; } }

      public string RelativeRoot { get { return _relativeRoot; } }

      public string RelativeRepositoryUri
      {
         get
         {
            if (_relativeRoot == "/") return RepositoryUri.AbsoluteUri;

            return RepositoryUri.AbsoluteUri.Replace(_relativeRoot, string.Empty);
         }
      }

      public string Branch
      {
         get
         {
            if (_repositoryUri != null)
            {
               string s = _repositoryUri.ToString();
               int idx = s.LastIndexOf("branch");
               if (idx != -1)
               {
                  idx = s.IndexOf('/', idx);
                  if (idx != -1)
                  {
                     string branchName = s.Substring(idx + 1);
                     return branchName;
                  }
               }
            }

            return "trunk";
         }
      }

      private static readonly char[] RepositoryTrimChars = {'/', ';'};

      public static string TrimRepositoryUrl(string url)
      {
         if (string.IsNullOrEmpty(url)) return null;

         return url.Trim(RepositoryTrimChars);
      }

      private void Initialize()
      {
         _svn = new SvnClient();
         SvnInfoEventArgs args;
         _svn.GetInfo(new SvnPathTarget(_workingCopyPath), out args);
         _root = new SvnUriTarget(args.Uri);
         string root = args.Uri.ToString();
         string repoRoot = args.RepositoryRoot.ToString();
         _relativeRoot = root.Substring(repoRoot.Length - 1);
         _repositoryUri = args.Uri;
      }

      public string GetDiff(long fromRev, long toRev)
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

      public string GetLocalDiff()
      {
         //SharpSvn doesn't do just "svn diff" and always requires some arguments, so I have nothing else to do other than run it directly

         var p = new Process
         {
            StartInfo = new ProcessStartInfo("svn", "diff")
            {
               UseShellExecute = false,
               RedirectStandardOutput = true,
               CreateNoWindow = true,
               WorkingDirectory = _workingCopyPath
            }
         };

         p.Start();

         var b = new StringBuilder();
         while (!p.StandardOutput.EndOfStream)
         {
            b.AppendLine(p.StandardOutput.ReadLine());
         }

         p.WaitForExit();

         return b.ToString();
      }

      private WorkItem ToWorkItem(SvnLogEventArgs logEntry)
      {
         var item = new WorkItem(
            logEntry.Revision.ToString(CultureInfo.InvariantCulture),
            logEntry.Author,
            logEntry.LogMessage,
            logEntry.Time);
         if (logEntry.ChangedPaths != null)
         {
            foreach (SvnChangeItem ci in logEntry.ChangedPaths)
            {
               item.ChangedFilePaths.Add(ci.Path);
            }
         }
         return item;
      }

      public IEnumerable<WorkItem> GetCommitedWorkItems(int maxRevisions)
      {
         var args = new SvnLogArgs {Limit = maxRevisions};

         Collection<SvnLogEventArgs> entries;
         _svn.GetLog(_root.Uri, args, out entries);

         if (entries != null && entries.Count > 0)
         {
            return entries.Select(ToWorkItem);
         }

         return null;
      }

      public IEnumerable<LocalWorkItem> GetLocalChanges()
      {
         var result = new List<LocalWorkItem>();

         Collection<SvnStatusEventArgs> changes;
         _svn.GetStatus(_workingCopyPath, out changes);
         if (changes != null && changes.Count > 0)
         {
            result.AddRange(
               changes.Select(change => new LocalWorkItem(change))
                  .Where(change => change.Status != LocalItemStatus.Unknown));
            return result.Count == 0 ? null : result;
         }

         return null;
      }

      public void Dispose()
      {
         
      }
   }
}
