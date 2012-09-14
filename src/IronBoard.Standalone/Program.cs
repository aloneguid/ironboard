using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IronBoard.Core;
using IronBoard.Core.Model;
using IronBoard.Core.WinForms;
using IronBoard.Standalone.Forms;

namespace IronBoard.Standalone
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         IbApplication.Initialise("c:\\dev\\msw", new CoreSettings());

         Application.Run(new PostCommitReviewForm());
         //Application.Run(new ReviewRequestsForm());
      }
   }
}
