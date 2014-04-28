using System;

namespace IronBoard.Core.Model
{
   public interface ICodeRepository : IDisposable
   {
      string Branch { get; }
   }
}
