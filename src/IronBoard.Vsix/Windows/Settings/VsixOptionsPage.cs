using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IronBoard.Core;
using Microsoft.VisualStudio.Shell;

namespace IronBoard.Vsix.Windows.Settings
{
   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1408:DoNotUseAutoDualClassInterfaceType")]
   [Guid("3FD88ED4-DAFC-4569-81D0-162EFEF297D9")]
   [ComVisible(true)]
   [ClassInterface(ClassInterfaceType.AutoDual)]
   class VsixOptionsPage : DialogPage, IServiceProvider
   {
      object IServiceProvider.GetService(Type serviceType)
      {
         return this.GetService(serviceType);
      }

      private UserControl _containerControl;
      private UserControl ContainerControl
      {
         get
         {
            if (_containerControl == null)
            {
               try
               {
                  _containerControl = new WinFormsToWpfBridgeControl(new Options());
                  _containerControl.Location = new Point(0, 0);
               }
               catch (Exception ex)
               {
                  Messages.ShowError(ex);
               }
            }
            return _containerControl;
         }
      }

      protected override IWin32Window Window
      {
         get { return ContainerControl; }
      }

      protected override void OnApply(DialogPage.PageApplyEventArgs e)
      {
         //todo: call container's apply
         base.OnApply(e);
      }
   }
}
