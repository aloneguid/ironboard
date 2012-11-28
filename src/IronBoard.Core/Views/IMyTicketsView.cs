using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.RBWebApi.Model;

namespace IronBoard.Core.Views
{
   interface IMyTicketsView
   {
      void UpdateList(IEnumerable<Review> myTickets, Exception error);
   }
}
