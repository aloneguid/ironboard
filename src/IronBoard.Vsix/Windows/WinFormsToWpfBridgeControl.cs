using System.Windows.Forms;
using WpfUserControl = System.Windows.Controls.UserControl;

namespace IronBoard.Vsix.Windows
{
   public partial class WinFormsToWpfBridgeControl : UserControl
   {
      public WinFormsToWpfBridgeControl(WpfUserControl wpfControl)
      {
         InitializeComponent();

         wpfHost.Child = wpfControl;
      }
   }
}
