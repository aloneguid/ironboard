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
      public static RBClient RBClient { get; private set; }

      public static SvnRepository SvnRepository { get; private set; }

      public static CoreSettings Settings { get; private set; }

      public static event Action<CoreSettings> SettingsChanged;

      public static ILoginPasswordView LoginView { get; set; }

      public static void Initialise(string solutionPath, CoreSettings settings)
      {
         if (solutionPath == null) throw new ArgumentNullException("solutionPath");
         if (settings == null) throw new ArgumentNullException("settings");

         Settings = settings;
         SvnRepository = new SvnRepository(solutionPath);
         RBClient = new RBClient(SvnRepository.RepositoryUri.ToString(), solutionPath, settings.AuthCookie);
         RBClient.AuthenticationRequired += OnAuthenticationRequired;
         RBClient.AuthCookieChanged += OnAuthCookieChanged;
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
