using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronBoard.Daemon.Model;

namespace IronBoard.Daemon.Application
{
   class ConsoleNotifier : INotifier
   {
      public void NotifyUser(NotificationLevel level, string format, params object[] parameters)
      {
         ConsoleColor oldColor = Console.ForegroundColor;
         ConsoleColor color;
         switch (level)
         {
            case NotificationLevel.Verbose:
               color = ConsoleColor.Green;
               break;
            case NotificationLevel.Warning:
               color = ConsoleColor.Yellow;
               break;
            case NotificationLevel.Error:
               color = ConsoleColor.Red;
               break;
            default:
               color = oldColor;
               break;
         }

         try
         {
            Console.ForegroundColor = color;

            Console.WriteLine(format, parameters);
         }
         finally
         {
            Console.ForegroundColor = oldColor;
         }
      }
   }
}
