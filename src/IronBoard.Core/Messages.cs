using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IronBoard.Core
{
   public static class Messages
   {
      public static void ShowError(string message)
      {
         MessageBox.Show(message, Strings.Message_Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
   }
}
