using System.Windows.Forms;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.WinForms
{
   public partial class ReviewEntityControl : UserControl
   {
      public ReviewEntityControl()
      {
         InitializeComponent();
      }

      public void Fill(Review review)
      {
         if(review != null)
         {
            review.Subject = Summary.Text;
            review.Description = Description.Text;
            review.TestingDone = Testing.Text;
            review.IsDraft = IsDraft.Checked;
         }
      }

      public void Display(Review review)
      {
         if(review != null)
         {
            Summary.Text = review.Subject ?? string.Empty;
            Description.Text = review.Description ?? string.Empty;
            Testing.Text = review.TestingDone ?? string.Empty;
            IsDraft.Checked = review.IsDraft;
         }
      }
   }
}
