using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IronBoard.Core.WinForms
{
   public partial class PeopleEntitySelector : UserControl
   {
      private PeopleEntitySelectorListForm _form;

      public PeopleEntitySelector()
      {
         InitializeComponent();
      }

      public void SetAllElements(string selectorTitle, IEnumerable<string> all)
      {
         _form = new PeopleEntitySelectorListForm(selectorTitle, all);
      }

      public IEnumerable<string> SelectedElements
      {
         get { return _form == null ? null : _form.SelectedElements; }
      }

      private void RenderSelected()
      {
         if(_form == null || _form.SelectedElements == null)
         {
            ToolTip.SetToolTip(SelectionText, string.Empty);
            SelectionText.Text = string.Empty;
         }
         else
         {
            string s = string.Join(",", _form.SelectedElements);
            ToolTip.SetToolTip(SelectionText, s);
            SelectionText.Text = s;
         }
      }

      private void Button_Click(object sender, EventArgs e)
      {
         if(_form != null && DialogResult.OK == _form.ShowDialog(this))
         {
            RenderSelected();
         }
      }
   }
}
