namespace IronBoard.Core.Model
{
   /// <summary>
   /// Source Code Manager capabilities
   /// </summary>
   public class ScmCapabilities
   {
      internal ScmCapabilities(bool slowHistoryFetch)
      {
         SlowHistoryFetch = slowHistoryFetch;
      }

      /// <summary>
      /// When true indicates that fetching commit history is slow and should only be used when needed
      /// </summary>
      public bool SlowHistoryFetch { get; private set; }
   }
}
