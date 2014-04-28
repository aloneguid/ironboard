using System;
using System.Collections;
using System.Collections.Generic;

namespace IronBoard.Core.Model
{
   public interface ICodeRepository : IDisposable
   {
      string ClientVersion { get; }

      string Branch { get; }

      /// <summary>
      /// Relative root of this repository according to where code diff is generated from
      /// </summary>
      string RelativeRoot { get; }

      Uri RemoteRepositoryUri { get; }

      string RelativeRepositoryUri { get; }

      string GetLocalDiff();

      string GetDiff(string from, string to);

      IEnumerable<WorkItem> GetHistory(int maxEntries);
   }
}
