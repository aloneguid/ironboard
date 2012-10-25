using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;
using IronBoard.RBWebApi.Model;
using System.Linq;
using System.Linq.Expressions;

namespace IronBoard.Core.WinForms
{
   public partial class ReviewRequests : UserControl, IReviewRequestsView
   {
      private readonly ReviewRequestsPresenter _presenter;

      public ReviewRequests()
      {
         _presenter = new ReviewRequestsPresenter(this);

         InitializeComponent();

         if (!DesignMode)
         {
            RefreshRequests();
         }
      }

      private void RefreshRequests()
      {
         ReloadReviews.Enabled = false;
         Task.Factory.StartNew(() =>
            {
               IEnumerable<Review> tickets = null;
               try
               {
                  tickets = IbApplication.RBClient.GetPersonalRequests();
               }
               catch(Exception ex)
               {
                  Messages.ShowError(ex);
               }
               finally
               {
                  UiScheduler.UiExecute(() => RenderTickets(tickets));
               }
            });
      }

      private void RenderTickets(IEnumerable<Review> tickets)
      {
         ReloadReviews.Enabled = true;
         Requests.Rows.Clear();
         if(tickets != null)
         {
            foreach(Review r in tickets.OrderBy(t => t.LastUpdated).Reverse())
            {
               Requests.Rows.Add(_presenter.FormatGridRow(r));
            }
         }
      }

      private void ReloadReviews_Click(object sender, EventArgs e)
      {
         RefreshRequests();
      }
   }
}
