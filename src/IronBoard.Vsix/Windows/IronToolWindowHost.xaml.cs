using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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

         WorkLog.SelectectionChanged += WorkLogSelectectionChanged;
      }

      void WorkLogSelectectionChanged(IEnumerable<Core.Model.WorkItem> items, int rmin, int rmax)
      {
         MyTickets.SetSelection(items, rmin, rmax);
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
               MyTickets.RefreshView();
               break;
         }
      }
   }
}
