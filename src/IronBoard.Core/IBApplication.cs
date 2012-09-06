using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using IronBoard.Core.Views;
using IronBoard.Core.WinForms;
using IronBoard.RBWebApi;

namespace IronBoard.Core
{
   public static class IBApplication
   {
      private static ILoginPasswordView _loginView;

      public static RBClient RBClient { get; private set; }

      public static event Action<string> AuthCookieChanged;

      public static void Initialise(string svnRepositoryPath, string workingCopyPath, ILoginPasswordView loginView, string oldCookie)
      {
         if (loginView == null) throw new ArgumentNullException("loginView");
         _loginView = loginView;
         RBClient = new RBClient(svnRepositoryPath, workingCopyPath, oldCookie);
         RBClient.AuthenticationRequired += OnAuthenticationRequired;
         RBClient.AuthCookieChanged += OnAuthCookieChanged;
      }

      static void OnAuthCookieChanged(string cookie)
      {
         if (AuthCookieChanged != null) AuthCookieChanged(cookie);
      }

      static void OnAuthenticationRequired(NetworkCredential cred)
      {
         NetworkCredential newCred = null;

         UiScheduler.UiExecute(() =>
         {
            newCred = _loginView.CollectCredential();
         }, true);

         if (newCred != null)
         {
            cred.UserName = newCred.UserName;
            cred.Password = newCred.Password;
         }
      }
   }
}
