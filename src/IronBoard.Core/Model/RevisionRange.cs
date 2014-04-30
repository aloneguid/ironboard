namespace IronBoard.Core.Model
{
   public class RevisionRange
   {
      public string From { get; private set; }

      public string To { get; private set; }

      public RevisionRange(string from, string to)
      {
         From = from;
         To = to;
      }
   }
}
