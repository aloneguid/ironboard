using System;

namespace IronBoard.Core.Model
{
   public interface ICodeRepository : IDisposable
   {
      string ClientVersion { get; }

      string Branch { get; }

      string GetLocalDiff();
   }
}
