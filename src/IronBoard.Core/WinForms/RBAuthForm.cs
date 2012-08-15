using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronBoard.RBWebApi;

namespace IronBoard.Core.WinForms
{
   public partial class RBAuthForm : Form
   {
      private readonly RBClient _api;

      public RBAuthForm()
      {
         InitializeComponent();
      }

      public RBAuthForm(RBClient api) : this()
      {
         if (api == null) throw new ArgumentNullException("api");

         _api = api;
      }

      private void Authenticate_Click(object sender, EventArgs e)
      {
         
      }

      private void Cancel_Click(object sender, EventArgs e)
      {
         //
      }

      private void Login_TextChanged(object sender, EventArgs e)
      {
         ValidateButtons();
      }

      private void Password_TextChanged(object sender, EventArgs e)
      {
         ValidateButtons();
      }

      private void ValidateButtons()
      {
         Authenticate.Enabled = Login.Text.Length > 0 && Password.Text.Length > 0;
      }
   }
}
