using System;
using System.Net;
using IronBoard.Core.Application;
using IronBoard.Core.Model;
using IronBoard.Core.Views;
using IronBoard.Core.WinForms;
using IronBoard.RBWebApi;

namespace IronBoard.Core
{
   public static class IbApplication
   {
      public static IRbClient RbClient { get; private set; }

      public static SvnRepository SvnRepository { get; private set; }

      public static CoreSettings Settings { get; private set; }

      public static event Action<CoreSettings> SettingsChanged;

      public static event Action<string> OnOpenBrowserWindow;

      public static ILoginPasswordView LoginView { get; set; }

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

         Settings = settings;
         SvnRepository = new SvnRepository(solutionPath);
         RbClient = client;
         RbClient.AuthenticationRequired += OnAuthenticationRequired;
         RbClient.AuthCookieChanged += OnAuthCookieChanged;
      }

      public static void OpenBrowserWindow(string url)
      {
         if (OnOpenBrowserWindow != null)
         {
            OnOpenBrowserWindow(url);
         }
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
