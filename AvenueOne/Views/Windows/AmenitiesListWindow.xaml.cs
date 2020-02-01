using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
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

namespace AvenueOne.ViewModels.WindowsViewModels
{
    /// <summary>
    /// Interaction logic for AmenitiesListWindow.xaml
    /// </summary>
    public partial class AmenitiesListWindow : Window
    {
        private PlutoContext _plutoContext;
        public AmenitiesListWindow(PlutoContext plutoContext)
        {
            InitializeComponent();

            this._plutoContext = plutoContext;

            LinkAmenityToRoomTypeCommand linkAmenityToRoomTypeCommand = new LinkAmenityToRoomTypeCommand(new UnitOfWork(_plutoContext),
                                                                                                                                                                                                    new WpfDisplayService());
            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            IAmenitiesListWindowViewModel amenitiesListWindowViewModel = new AmenitiesListWindowViewModel(this,
                                                                                                                                                                                        closeWindowCommand,
                                                                                                                                                                                        _plutoContext.Amenities.Local,
                                                                                                                                                                                        new RoomType(),
                                                                                                                                                                                        linkAmenityToRoomTypeCommand);
            DataContext = amenitiesListWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
