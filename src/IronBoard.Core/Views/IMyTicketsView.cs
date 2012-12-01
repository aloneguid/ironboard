using System;
using System.Collections.Generic;
using IronBoard.Core.Wpf.Data;

namespace IronBoard.Core.Views
{
   interface IMyTicketsView
   {
      void UpdateList(IEnumerable<MyTicketData> myTickets, Exception error);

      void UpdateBusyStatus(string busyMessage);

      void FinishTicketUpdate(Exception ex);
   }
}
