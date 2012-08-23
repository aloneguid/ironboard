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
         Users.SetAllElements("select users", users.Select(u => u.Username));
         Groups.SetAllElements("select groups", groups.Select(g => g.Name));
      }

      public void Fill(Review review, PostCommitReviewPresenter presenter)
      {
         if(review != null)
         {
            review.Subject = Summary.Text;
            review.Description = Description.Text;
            review.TestingDone = Testing.Text;
            review.IsDraft = IsDraft.Checked;
            review.TargetUsers.Clear();
            review.TargetGroups.Clear();
            foreach (User u in presenter.AsUsers(Users.SelectedElements))
            {
               review.TargetUsers.Add(u);
            }
            foreach(UserGroup g in presenter.AsGroups(Groups.SelectedElements))
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
            IsDraft.Checked = review.IsDraft;
         }
      }
   }
}
