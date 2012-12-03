using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace IronBoard.Vsix.Windows
{
   [Guid("757397F1-BA52-4BC7-8910-5EC1BEFED713")]
   public class IronToolWindow : ToolWindowPane
   {
      //public static IReviewRequestsView Panel { get; private set; }
      private IronToolWindowHost _content;

      public IronToolWindow() : base(null)
      {
         Caption = Resources.IronToolWindow_Title;
         //BitmapResourceID = 400;
         //BitmapIndex = 0;
      }

      public override object Content
      {
         get { return _content ?? (_content = new IronToolWindowHost()); }
         set { base.Content = value; }
      }
   }
}
