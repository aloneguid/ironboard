using System.Collections.Generic;
using System.Windows.Controls;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for ReviewerSelector.xaml
   /// </summary>
   public partial class ReviewerSelector : UserControl
   {
      public ReviewerSelector()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Gets or sets reviewers (users or groups)
      /// </summary>
      public IEnumerable<Reviewer> Reviewers
      {
         get
         {
            if (Combo.SelectedItems != null)
            {
               var result = new List<Reviewer>();
               foreach (object o in Combo.SelectedItems)
               {
                  result.Add((Reviewer)o);
               }
               return result;
            }
            return null;
         }
         set { Combo.ItemsSource = value; }
      }
   }
}
