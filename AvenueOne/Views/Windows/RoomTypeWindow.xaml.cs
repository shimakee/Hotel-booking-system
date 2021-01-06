using AvenueOne.ViewModels.TabViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AvenueOne.Views.Windows
{
    /// <summary>
    /// Interaction logic for RoomTypeWindow.xaml
    /// </summary>
    public partial class RoomTypeWindow : Window
    {
        RoomTypeViewModel _roomTypeViewModel;

        public RoomTypeWindow(RoomTypeViewModel roomTypeViewModel)
        {
            InitializeComponent();

            formControl.RoomTypeViewModel = roomTypeViewModel;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
