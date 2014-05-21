using System;

namespace IronBoard.Core.Model
{
   /// <summary>
   /// Represents revision range
   /// </summary>
   public class RevisionRange : ICloneable
   {
      /// <summary>
      /// Minimum revision (inclusive)
      /// </summary>
      public string From { get; set; }

      /// <summary>
      /// Maximum revision (inclusive)
      /// </summary>
      public string To { get; set; }

      public RevisionRange(string from, string to)
      {
         From = from;
         To = to;
      }

      public object Clone()
      {
         return new RevisionRange(From, To);
      }
   }
}
