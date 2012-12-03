using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using IronBoard.Core;
using IronBoard.Core.Model;
using IronBoard.Core.WinForms;
using IronBoard.Vsix.Windows;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Process = System.Diagnostics.Process;

namespace IronBoard.Vsix.Package
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
   [Guid(GuidList.guidIronBoard_VsixPkgString)]
   [ProvideToolWindow(typeof(IronToolWindow),
        Style = VsDockStyle.Tabbed,
        Orientation = ToolWindowOrientation.Right)]
   [ComVisible(true)]
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

         OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
         if (null != mcs)
         {
            // Create the command for the tool window
            CommandID toolwndCommandID = new CommandID(GuidList.guidIronBoard_VsixCmdSet, (int)PkgCmdIDList.cmdidIronToolWindow);
            MenuCommand menuToolWin = new MenuCommand(ShowToolWindow, toolwndCommandID);
            mcs.AddCommand(menuToolWin);
         }

         _settingsStore = GetWritableSettingsStore(SettingsRoot);
         InitialiseIbApp();
         InitializeShell();
      }

      private void InitialiseIbApp()
      {
         UiScheduler.InitializeUiContext();
         IbApplication.LoginView = new RBAuthForm(null);
         IbApplication.SettingsChanged += IbSettingsChanged;
         IbApplication.OnOpenBrowserWindow += OnOpenBrowserWindow;
         IbApplication.OnOpenFile += IbApplication_OnOpenFile;
      }

      void IbApplication_OnOpenFile(string fullPath)
      {
         var p = new System.Diagnostics.Process();
         p.StartInfo.UseShellExecute = true;
         p.StartInfo.FileName = fullPath;
         p.Start();
      }

      void OnOpenBrowserWindow(string url, bool external)
      {
         if (!external)
         {
            var dte = GetGlobalService(typeof (DTE)) as DTE2;
            var itemOps = dte.ItemOperations;
            itemOps.Navigate(url);
         }
         else
         {
            var p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = string.Format(url);
            p.Start();
         }
      }

      private void OpenIbApp()
      {
         string settingsString = ReadOption(SettingsKey);
         CoreSettings settings = settingsString == null
                                    ? null
                                    : settingsString.TrivialDeserialize<CoreSettings>();
         if (settings == null) settings = new CoreSettings();
         //IbApplication.BackgroundBrush = (SolidColorBrush)VsBrushes.ToolWindowBackgroundKey;
         IbApplication.Initialise(ConfigFolder, settings);
         //IbApplication.Initialise(ConfigFolder, settings, RbFactory.CreateMockedClient());
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

      private void ShowToolWindow(object o, EventArgs args)
      {
         ShowToolWindow();
      }

      private void ShowToolWindow()
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

      void IbSettingsChanged(CoreSettings settings)
      {
         SaveOption(SettingsKey, settings.TrivialSerialize());
      }
   }
}