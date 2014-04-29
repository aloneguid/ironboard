using System;
using System.Net;
using System.Windows.Media;
using IronBoard.Core.Application;
using IronBoard.Core.Model;
using IronBoard.Core.Views;
using IronBoard.Core.WinForms;
using IronBoard.RBWebApi;
using IronBoard.RBWebApi.Application;

namespace IronBoard.Core
{
   public static class IbApplication
   {
      public static IRbClient RbClient { get; private set; }

      public static ICodeRepository CodeRepository { get; private set; }

      public static CoreSettings Settings { get; private set; }

      public static event Action<CoreSettings> SettingsChanged;

      public static event Action<string, bool> OnOpenBrowserWindow;

      public static event Action<string> OnOpenFile;

      public static ILoginPasswordView LoginView { get; set; }

      public static string SolutionPath { get; private set; }

      public static ScmProvider ScmProvider { get; private set; }

      public static ReviewBoardRc Config { get; private set; }

      public static void Initialise(string solutionPath, CoreSettings settings)
      {
         Initialise(solutionPath, settings,
                    RbFactory.CreateHttpClient(solutionPath,
                                               settings == null ? null : settings.AuthCookie));
      }

      public static void Initialise(string solutionPath, CoreSettings settings, IRbClient client)
      {
         if (solutionPath == null) throw new ArgumentNullException("solutionPath");
         if (settings == null) throw new ArgumentNullException("settings");

         SolutionPath = solutionPath;
         Settings = settings;

         Config = new ReviewBoardRc(solutionPath);
         ScmProvider = new ScmProviderDetector(solutionPath).DetectProvider();
         switch (ScmProvider)
         {
            case ScmProvider.None:
               throw new ApplicationException("not under source control");
            case ScmProvider.Git:
               CodeRepository = new GitRepository(solutionPath);
               break;
            case ScmProvider.Svn:
               CodeRepository = new SvnRepository(solutionPath);
               break;
         }

         RbClient = client;
         RbClient.AuthenticationRequired += OnAuthenticationRequired;
         RbClient.AuthCookieChanged += OnAuthCookieChanged;
      }

      public static void OpenBrowserWindow(string url, bool external)
      {
         if (OnOpenBrowserWindow != null)
         {
            OnOpenBrowserWindow(url, external);
         }
      }

      public static void OpenFile(string fullPath)
      {
         if (OnOpenFile != null)
         {
            OnOpenFile(fullPath);
         }
      }

      private static Brush _backgroundBrush;
      public static Brush BackgroundBrush
      {
         get
         {
            if(_backgroundBrush == null)
            {
               _backgroundBrush = System.Windows.SystemColors.ControlBrush;
            }
            return _backgroundBrush;
         }
         set { _backgroundBrush = value; }
      }

      static void OnAuthCookieChanged(string cookie)
      {
         Settings.AuthCookie = cookie;
         ThrowSettingsChanged();
      }

      static void OnAuthenticationRequired(NetworkCredential cred)
      {
         NetworkCredential newCred = null;

         UiScheduler.UiExecute(() =>
         {
            newCred = LoginView.CollectCredential();
         }, true);

         if (newCred != null)
         {
            cred.UserName = newCred.UserName;
            cred.Password = newCred.Password;
         }
      }

      static void ThrowSettingsChanged()
      {
         if(SettingsChanged != null)
         {
            SettingsChanged(Settings);
         }
      }
   }
}
