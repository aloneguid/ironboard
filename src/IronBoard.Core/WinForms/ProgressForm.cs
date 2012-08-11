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
   public partial class ProgressForm<T> : Form
   {
      private readonly Control _parent;
      private readonly string _message;
      private readonly Func<T> _workAction;
      private readonly Action<T, Exception> _callbackAction;
      private Exception _threadError;
      private T _result;

      public ProgressForm()
      {
         InitializeComponent();

         this.Shown += ProgressFormShown;
      }

      void ProgressFormShown(object sender, EventArgs e)
      {
         Message.Text = _message ?? "wait...";

         var t = Task.Factory.StartNew(() =>
            {
               T result;
               try
               {
                  result = _workAction();
               }
               catch(Exception ex)
               {
                  result = default(T);
                  _threadError = ex;
               }

               UiScheduler.UiExecute(() =>
                  {
                     _parent.Enabled = true;
                     Close();
                     if(_callbackAction != null) _callbackAction(result, _threadError);
                  });

            }, TaskCreationOptions.LongRunning);
      }

      private ProgressForm(Control parent, string message,
         Func<T> workAction, Action<T, Exception> callbackAction) : this()
      {
         _parent = parent;
         _message = message;
         _workAction = workAction;
         _callbackAction = callbackAction;
      }

      public static void Execute(Control parent, string message,
         Func<T> workAction, Action<T, Exception> callbackAction)
      {
         if (parent == null) throw new ArgumentNullException("parent");
         if (workAction == null) throw new ArgumentNullException("workAction");

         var me = new ProgressForm<T>(parent, message, workAction, callbackAction);
         me.ShowDialog(parent);
      }
   }
}
