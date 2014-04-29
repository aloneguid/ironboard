using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using IronBoard.Core;
using IronBoard.Core.Model;
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

      void WorkLogSelectectionChanged(IEnumerable<WorkItem> items, int rmin, int rmax)
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

         DisplayScmIcon();
      }

      private void DisplayScmIcon()
      {
         SourceProviderImage.Visibility = Visibility.Visible;

         switch (IbApplication.ScmProvider)
         {
            case ScmProvider.None:
               SourceProviderImage.Visibility = Visibility.Hidden;
               break;
            case ScmProvider.Svn:
               SourceProviderImage.Source = new BitmapImage(
                  new Uri(@"pack://application:,,,/" +
                          Assembly.GetExecutingAssembly().GetName().Name +
                          ";component/Resources/scm_svn.png",
                     UriKind.Absolute));
               break;
            case ScmProvider.Git:
               SourceProviderImage.Source = new BitmapImage(
                  new Uri(@"pack://application:,,,/" +
                          Assembly.GetExecutingAssembly().GetName().Name +
                          ";component/Resources/scm_git.png",
                     UriKind.Absolute));
               break;
         }
      }

      private void Tabs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         if ((e.Source is TabControl) && PendingChangesTab.IsSelected)
         {
            PendingChanges.RefreshView();
         }
      }
   }
}
