using System.Runtime.InteropServices;
using IronBoard.Core.Views;
using IronBoard.Core.WinForms;
using Microsoft.VisualStudio.Shell;

namespace IronBoard.Vsix.Windows
{
   [Guid("757397F1-BA52-4BC7-8910-5EC1BEFED713")]
   public class IronToolWindow : ToolWindowPane
   {
      //public static IReviewRequestsView Panel { get; private set; }

      public IronToolWindow() : base(null)
      {
         Caption = Resources.IronToolWindow_Title;
         BitmapResourceID = 402;
         BitmapIndex = 0;
         base.Content = new IronToolWindowHost();
      }
   }
}
