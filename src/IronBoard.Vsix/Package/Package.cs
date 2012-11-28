using System;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using IronBoard.Core;
using IronBoard.Core.Model;
using IronBoard.Core.WinForms;
using IronBoard.RBWebApi;
using IronBoard.Vsix.Windows;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace IronBoard.Vsix.Package
{
   /// <summary>
   /// This is the class that implements the package exposed by this assembly.
   ///
   /// The minimum requirement for a class to be considered a valid package for Visual Studio
   /// is to implement the IVsPackage interface and register itself with the shell.
   /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
   /// to do it: it derives from the Package class that provides the implementation of the 
   /// IVsPackage interface and uses the registration attributes defined in the framework to 
   /// register itself and its components with the shell.
   /// </summary>
   // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
   // a package.
   [PackageRegistration(UseManagedResourcesOnly = true)]
   // This attribute is used to register the informations needed to show the this package
   // in the Help/About dialog of Visual Studio.
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
   // This attribute is needed to let the shell know that this package exposes some menus.
   [ProvideMenuResource("Menus.ctmenu", 1)]
   [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
   [Guid(GuidList.guidIronBoard_VsixPkgString)]
   [ProvideToolWindow(typeof(IronToolWindow))]
   public partial class Package : Microsoft.VisualStudio.Shell.Package, IVsSolutionEvents
   {
      /// <summary>
      /// Default constructor of the package.
      /// Inside this method you can place any initialization code that does not require 
      /// any Visual Studio service because at this point the package object is created but 
      /// not sited yet inside Visual Studio environment. The place to do all the other 
      /// initialization is the Initialize method.
      /// </summary>
      public Package()
      {
      }

      #region Package Members

      /// <summary>
      /// Initialization of the package; this method is called right after the package is sited, so this is the place
      /// where you can put all the initilaization code that rely on services provided by VisualStudio.
      /// </summary>
      protected override void Initialize()
      {
         base.Initialize();

         // Add our command handlers for menu (commands must exist in the .vsct file)
         OleMenuCommandService mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
         if (null != mcs)
         {
            // Create the command for the menu item.
            CommandID menuCommandID = new CommandID(GuidList.guidIronBoard_VsixCmdSet,
                                                    (int) PkgCmdIDList.cmdidReviewBoard);
            MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
            mcs.AddCommand(menuItem);
         }

         _settingsStore = GetWritableSettingsStore(SettingsRoot);
         InitialiseIbApp();
         InitializeShell();
      }

      private void InitialiseIbApp()
      {
         IbApplication.SettingsChanged += IbSettingsChanged;
         IbApplication.OnOpenBrowserWindow += OnOpenBrowserWindow;
      }

      void OnOpenBrowserWindow(string url)
      {
         if (url != null)
         {
            IVsCommandWindow service = (IVsCommandWindow) this.GetService(typeof (SVsCommandWindow));
            if (service != null)
            {
               string command = string.Format("File.OpenFile \"{0}\"", url);
               service.ExecuteCommand(command);
            }
         }
      }

      private void OpenIbApp()
      {
         string settingsString = ReadOption(SettingsKey);
         CoreSettings settings = settingsString == null
                                    ? null
                                    : settingsString.TrivialDeserialize<CoreSettings>();
         if (settings == null) settings = new CoreSettings();
         IbApplication.Initialise(ConfigFolder, settings);
      }

      private void InitializeSolution()
      {
         if (!ConfigExists)
         {
            Extension.State = GlobalState.NoConfigFile;
         }
         else
         {
            OpenIbApp();
            Extension.State = GlobalState.Operational;
         }
      }

      private uint _solutionEventsCookie;
      private void InitializeShell()
      {
         IVsSolution2 solution = ServiceProvider.GlobalProvider.GetService(typeof(SVsSolution)) as IVsSolution2;
         if (solution != null)
         {
            solution.AdviseSolutionEvents(this, out _solutionEventsCookie);
         }

      }

      private void ShowToolWindow(bool show)
      {
         //this method will show the window if it's not active or bring it to front if it's collapsed
         ToolWindowPane window = this.FindToolWindow(typeof(IronToolWindow), 0, true);
         if ((null == window) || (null == window.Frame))
         {
            throw new NotSupportedException();
         }
         IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
         Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
      }

      #endregion

      /// <summary>
      /// This function is the callback used to execute a command when the a menu item is clicked.
      /// See the Initialize method to see how the menu item is associated to this function using
      /// the OleMenuCommandService service and the MenuCommand class.
      /// </summary>
      private void MenuItemCallback(object sender, EventArgs e)
      {
         DirectoryInfo di = SolutionDirectory;
         if (di != null)
         {
            string configFolder = RBUtils.FindConfigFolder(di.FullName);
            if (configFolder == null)
            {
               Messages.ShowError("config file not found");
            }
            else
            {
               try
               {
                  var form = new PostCommitReviewForm();
                  form.ShowDialog();
               }
               catch (Exception ex)
               {
                  Messages.ShowError(ex.Message +
                                     Environment.NewLine + Environment.NewLine +
                                     ex.StackTrace);
               }
            }
         }
         else
         {
            Messages.ShowError("Open a solution first");
         }
      }

      void IbSettingsChanged(CoreSettings settings)
      {
         SaveOption(SettingsKey, settings.TrivialSerialize());
      }
   }
}