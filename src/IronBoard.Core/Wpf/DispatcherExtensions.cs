using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Windows.Threading
{
   static class DispatcherExtensions
   {
      public static void Push(this Dispatcher dispatcher, Action a)
      {
         var th = new ThreadStart(a);
         dispatcher.Invoke(th);
      }
   }
}
