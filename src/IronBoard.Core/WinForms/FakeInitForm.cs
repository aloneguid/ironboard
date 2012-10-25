using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
