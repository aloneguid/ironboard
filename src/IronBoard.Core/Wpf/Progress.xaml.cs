using System;
using System.Windows;
using System.Windows.Controls;
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
               if (ProgressTitle != null) ProgressText.Text = ProgressTitle;
            }
            else
            {
               if(_timer != null)
               {
                  _timer.Tick -= ProgressTick;
                  _timer.Stop();
                  _timer = null;
               }
               Visibility = Visibility.Collapsed;
            }
         }
         get { return Visibility == Visibility.Visible; }
      }

      public string ProgressTitle { get; set; }

      void ProgressTick(object sender, EventArgs e)
      {
         Char.Text = _elements[_idx++];
         if (_idx >= _elements.Length) _idx = 0;
      }
   }
}
