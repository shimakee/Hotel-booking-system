using AvenueOne.Core.Models;
using AvenueOne.ViewModels.TabViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Amenities> _amenities;

        public RoomTypeWindow(RoomTypeViewModel roomTypeViewModel, ObservableCollection<Amenities> amenities)
        {
            InitializeComponent();

            _amenities = amenities;
            _roomTypeViewModel = roomTypeViewModel;

            formControl.RoomTypeViewModel = roomTypeViewModel;
            formControl.AmenitiesCollection = amenities;
            formControl.ClassCommand = roomTypeViewModel.CreateClassCommand;
            formControl.AttachDetach.Visibility = Visibility.Collapsed;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
