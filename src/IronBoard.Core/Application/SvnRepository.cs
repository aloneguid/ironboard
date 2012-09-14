using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using IronBoard.Core.Model;
using SharpSvn;

namespace IronBoard.Core.Application
{
   public class SvnRepository
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

      public Uri RepositoryUri
      {
         get { return _repositoryUri; }
      }

      public string RelativeRoot
      {
         get { return _relativeRoot; }
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

      public IEnumerable<WorkItem> GetCommitedWorkItems(int maxRevisions)
      {
         var args = new SvnLogArgs { Limit = maxRevisions };

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
   }
}
