using AvenueOne.Core.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
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
    /// Interaction logic for AmenitiesWindow.xaml
    /// </summary>
    public partial class AmenitiesWindow : Window
    {
        public IAmenitiesWindowViewModel ViewModel { get; set; }
        private PlutoContext _plutoContext;
        public AmenitiesWindow(PlutoContext plutoContext)
        {
            InitializeComponent();

            this._plutoContext = plutoContext;

            IAmenitiesWindowViewModel amenititesWindowViewModel = new AmenitiesWindowViewModel(this, new Amenities());
            this.ViewModel = amenititesWindowViewModel;
            this.DataContext = amenititesWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
