using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IronBoard.Core.WinForms
{
   public partial class ProgressForm : Form
   {
      private readonly string _message;
      private readonly Action _action;

      public ProgressForm()
      {
         InitializeComponent();

         this.Shown += ProgressForm_Shown;
      }

      void ProgressForm_Shown(object sender, EventArgs e)
      {
         Message.Text = _message;

         var t = Task.Factory.StartNew(_action);

         t.Wait();

         Close();
      }

      private ProgressForm(string message, Action action) : this()
      {
         _message = message;
         _action = action;
      }

      public static void Execute<T>(Control parent, string message)
      {
         //var me = new ProgressForm(message, action);
         //me.ShowDialog(parent);
      }
   }
}
