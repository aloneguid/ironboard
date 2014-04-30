using System;
using System.Net;
using System.Windows.Forms;
using IronBoard.Core.Views;

namespace IronBoard.Core.WinForms
{
   public partial class RBAuthForm : Form, ILoginPasswordView
   {
      private readonly IWin32Window _owner;

      public RBAuthForm()
      {
         InitializeComponent();
      }

      public RBAuthForm(IWin32Window owner) : this()
      {
         _owner = owner;
      }

      public NetworkCredential Credential
      {
         get
         {
            return new NetworkCredential(Login.Text, Password.Text);
         }
      }

      private void Authenticate_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.OK;
         Close();
      }

      private void Cancel_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.Cancel;
         Close();
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

      public NetworkCredential CollectCredential()
      {
         if(DialogResult.OK == ShowDialog(_owner))
         {
            return Credential;
         }

         return null;
      }

      private void TextKeyUp(object sender, KeyEventArgs e)
      {
         if(e.KeyCode == Keys.Return && Authenticate.Enabled)
         {
            Authenticate_Click(this, null);
         }
      }
   }
}
