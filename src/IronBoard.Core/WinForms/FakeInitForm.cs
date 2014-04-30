using System;
using System.Windows.Forms;

namespace IronBoard.Core.WinForms
{
   public partial class FakeInitForm : Form
   {
      public FakeInitForm()
      {
         InitializeComponent();

         Shown += FakeInitForm_Shown;
      }

      void FakeInitForm_Shown(object sender, EventArgs e)
      {
         UiScheduler.InitializeUiContext();

         Close();
      }
   }
}
