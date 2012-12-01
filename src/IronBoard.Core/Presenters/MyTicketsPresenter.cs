using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronBoard.Core.Views;
using IronBoard.Core.Wpf.Data;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Presenters
{
   class MyTicketsPresenter
   {
      private readonly IMyTicketsView _view;

      public MyTicketsPresenter(IMyTicketsView view)
      {
         _view = view;
      }

      public void ReloadData()
      {
         IEnumerable<Review> myTickets = null;
         Exception ex = null;
         Task.Factory.StartNew(() =>
            {
               try
               {
                  myTickets = IbApplication.RbClient.GetPersonalRequests();
               }
               catch (Exception ex1)
               {
                  ex = ex1;
               }
               _view.UpdateList(myTickets == null ? null : myTickets.Select(t => new MyTicketData(t)), ex);
            });
      }

      public void OpenInBrowser(Review r, bool external)
      {
         string url = string.Format("{0}r/{1}", IbApplication.RbClient.ServerUri, r.Id);
         IbApplication.OpenBrowserWindow(url, external);
      }

      public void Delete(Review r)
      {
         IbApplication.RbClient.Delete(r.Id);
      }

      public void UpdateTicket(Review r, long fromRev, long toRev)
      {
         Task.Factory.StartNew(() =>
            {
               Exception ex = null;
               try
               {
                  _view.UpdateBusyStatus(Strings.MyTickets_Update_GeneratingDiff);
                  string diffText = IbApplication.SvnRepository.GetDiff(fromRev, toRev);

                  _view.UpdateBusyStatus(Strings.MyTickets_Update_Diff);
                  IbApplication.RbClient.AttachDiff(r, IbApplication.SvnRepository.RelativeRoot, diffText);
                  IbApplication.RbClient.MakePublic(r);

                  string url = string.Format("{0}r/{1}", IbApplication.RbClient.ServerUri, r.Id);
                  IbApplication.OpenBrowserWindow(url, false);
               }
               catch (Exception ex1)
               {
                  ex = ex1;
               }
               _view.FinishTicketUpdate(ex);
            });
      }
   }
}
