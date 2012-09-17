using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IronBoard.Core.Presenters;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.WinForms
{
   public partial class ReviewEntityControl : UserControl
   {
      public ReviewEntityControl()
      {
         InitializeComponent();
      }

      public void SetData(IEnumerable<User> users, IEnumerable<UserGroup> groups)
      {
         Users.SetAllElements("select users", users);
         Groups.SetAllElements("select groups", groups);
      }

      public void Fill(Review review, PostCommitReviewPresenter presenter)
      {
         if(review != null)
         {
            review.Subject = Summary.Text;
            review.Description = Description.Text;
            review.TestingDone = Testing.Text;
            review.IsDraft = IsDraft.Checked;
            review.BugsClosed = Bugs.Text;
            review.TargetUsers.Clear();
            review.TargetGroups.Clear();
            foreach (User u in Users.SelectedElements)
            {
               review.TargetUsers.Add(u);
            }
            foreach(UserGroup g in Groups.SelectedElements)
            {
               review.TargetGroups.Add(g);
            }
         }
      }

      public void Display(Review review)
      {
         if(review != null)
         {
            Summary.Text = review.Subject ?? string.Empty;
            Description.Text = review.Description ?? string.Empty;
            Testing.Text = review.TestingDone ?? string.Empty;
            Bugs.Text = review.BugsClosed ?? string.Empty;
            IsDraft.Checked = review.IsDraft;
         }
      }
   }
}
