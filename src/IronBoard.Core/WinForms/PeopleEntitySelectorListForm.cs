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
   public partial class PeopleEntitySelectorListForm : Form
   {
      public PeopleEntitySelectorListForm()
      {
         InitializeComponent();
      }

      public PeopleEntitySelectorListForm(string selectorTitle, IEnumerable<string> all) : this()
      {
         Text = selectorTitle;
         ElementsList.Items.Clear();
         if(all != null)
         {
            foreach(string el in all)
            {
               ElementsList.Items.Add(el);
            }
         }
      }

      public IEnumerable<string> SelectedElements
      {
         get
         {
            var lst = new List<string>();
            foreach(object item in ElementsList.CheckedItems)
            {
               lst.Add(item.ToString());
            }
            return lst;
         }
      }

      private void OkButton_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.OK;
         Close();
      }

      private void CancelButton_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.Cancel;
         Close();
      }
   }
}
