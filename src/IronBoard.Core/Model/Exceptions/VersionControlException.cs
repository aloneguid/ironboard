using System;

namespace IronBoard.Core.Model.Exceptions
{
   public class VersionControlException : Exception
   {
      public VersionControlException(string format, params object[] parameters) : base(string.Format(format, parameters))
      {
         
      }
   }
}
