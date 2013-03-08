using System.Windows;
using System.Windows.Controls;
using IronBoard.Core;

namespace IronBoard.Vsix.Windows.Settings
{
   /// <summary>
   /// Interaction logic for Options.xaml
   /// </summary>
   public partial class Options : UserControl
   {
      public Options()
      {
         InitializeComponent();
      }

      private void ResetCreds_OnClick(object sender, RoutedEventArgs e)
      {
         IbApplication.RbClient.AuthCookie = null;
         IbApplication.Settings.AuthCookie = null;
         ResetCreds.IsEnabled = false;
      }
   }
}
