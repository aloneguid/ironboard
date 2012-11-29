using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            //todo:
            return null;
         }
         set { Combo.ItemsSource = value; }
      }
   }
}
