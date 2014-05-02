using System;
using System.Collections.Generic;

namespace IronBoard.Core.Model
{
   public interface ICodeRepository : IDisposable
   {
      string ClientVersion { get; }

      string Branch { get; }

      /// <summary>
      /// Relative root of this repository according to where code diff is generated from.
      /// <example>
      /// Remote repository: https://ironboard.svn.codeplex.com/svn/trunk/
      /// This value: /trunk/
      /// Because https://ironboard.svn.codeplex.com/svn/ is a root for all projects (or in this case trunk and branches)
      /// /trunk/ is a root the diff is generated from
      /// </example>
      /// </summary>
      string RelativeRoot { get; }

      Uri RemoteRepositoryUri { get; }

      string GetLocalDiff();

      string GetDiff(RevisionRange range);

      IEnumerable<WorkItem> GetHistory(int maxEntries);
   }
}
