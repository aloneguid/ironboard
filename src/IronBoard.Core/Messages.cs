using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IronBoard.Core
{
   public static class Messages
   {
      public static void ShowInfo(string message)
      {
         MessageBox.Show(message, Strings.Message_Title_Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      public static void ShowError(string message)
      {
         MessageBox.Show(message, Strings.Message_Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      public static void ShowError(Exception ex)
      {
         ShowError(ex.Message +
                   Environment.NewLine + Environment.NewLine +
                   ex.StackTrace);
      }
   }
}
