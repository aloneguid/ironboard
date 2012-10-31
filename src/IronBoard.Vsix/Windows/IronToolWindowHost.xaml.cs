using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Strings = IronBoard.Vsix.Resources;

namespace IronBoard.Vsix.Windows
{
   /// <summary>
   /// Interaction logic for IronToolWindowHost.xaml
   /// </summary>
   public partial class IronToolWindowHost : UserControl, IGlobalPanel
   {
      public IronToolWindowHost()
      {
         InitializeComponent();

         Extension.Panel = this;

         UpdateState();
      }

      public void UpdateState()
      {
         Tabs.Visibility = Extension.State == GlobalState.Operational
                              ? Visibility.Visible
                              : Visibility.Hidden;
         Error.Visibility = Extension.State != GlobalState.Operational
                               ? Visibility.Visible
                               : Visibility.Hidden;
         switch (Extension.State)
         {
            case GlobalState.NoSolutionOpen:
               Error.Content = Strings.Error_NoSolution;
               break;
            case GlobalState.NoConfigFile:
               Error.Content = Strings.Error_ConfigNotFound;
               break;
           case GlobalState.Operational:
               WorkLog.RefreshView();
               break;
         }
      }
   }
}
