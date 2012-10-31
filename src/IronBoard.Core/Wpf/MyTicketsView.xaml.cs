using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IronBoard.Core.Presenters;
using IronBoard.Core.Views;

namespace IronBoard.Core.Wpf
{
   /// <summary>
   /// Interaction logic for MyTicketsView.xaml
   /// </summary>
   public partial class MyTicketsView : UserControl, IMyTicketsView
   {
      private readonly MyTicketsPresenter _presenter;

      public MyTicketsView()
      {
         InitializeComponent();
         _presenter = new MyTicketsPresenter(this);
      }

      public void RefreshView()
      {
         
      }

      private void Refresh_Click(object sender, RoutedEventArgs e)
      {

      }
   }
}
