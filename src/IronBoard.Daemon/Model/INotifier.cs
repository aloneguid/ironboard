using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBoard.Daemon.Model
{
   interface INotifier
   {
      void NotifyUser(NotificationLevel level, string format, params object[] parameters);
   }
}
