using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.WinForms
{
   public partial class PeopleEntitySelectorListForm : Form
   {
      public PeopleEntitySelectorListForm()
      {
         InitializeComponent();
      }

      public PeopleEntitySelectorListForm(string selectorTitle, IEnumerable<Reviewer> all) : this()
      {
         Text = selectorTitle;
         ElementsList.Items.Clear();
         if(all != null)
         {
            foreach(Reviewer el in all)
            {
               ElementsList.Items.Add(el);
            }
         }
      }

      public IEnumerable<Reviewer> SelectedElements
      {
         get
         {
            var lst = new List<Reviewer>();
            foreach(Reviewer item in ElementsList.CheckedItems)
            {
               lst.Add(item);
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
