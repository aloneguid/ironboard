using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for Progress.xaml
   /// </summary>
   public partial class Progress : UserControl
   {
      private DispatcherTimer _timer;
      private int _idx;
      //private readonly string[] _elements = new[] { "/", "-", "\\", "|" };
      //private readonly string[] _elements = new[] { "←", "↖", "↑", "↗", "→", "↘", "↓", "↙" };
      //private readonly string[] _elements = new[] { "◡◡", "⊙⊙", "◠◠" };
      private readonly string[] _elements = new[] { ".", "..", "...", "....", "...", ".." };

      public Progress()
      {
         InitializeComponent();

         IsInProgress = false;
      }

      public bool IsInProgress
      {
         set
         {
            if (value)
            {
               if (_timer == null)
               {
                  _timer = new DispatcherTimer();
                  _timer.Tick += ProgressTick;
                  _timer.Interval = TimeSpan.FromMilliseconds(100);
                  _timer.Start();
               }
               Visibility = Visibility.Visible;
            }
            else
            {
               if(_timer != null)
               {
                  _timer.Tick -= ProgressTick;
                  _timer.Stop();
                  _timer = null;
               }
               Visibility = Visibility.Hidden;
            }
         }
      }

      void ProgressTick(object sender, EventArgs e)
      {
         Char.Text = _elements[_idx++];
         if (_idx >= _elements.Length) _idx = 0;
      }
   }
}
