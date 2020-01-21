using AvenueOne.Core.Models;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands.RoomCommands;
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
    /// Interaction logic for AddRoomTypeWindow.xaml
    /// </summary>
    public partial class AddRoomTypeWindow : Window
    {
        public IRoomTypeWindowViewModel ViewModel;
        private PlutoContext _plutoContext;
        public AddRoomTypeWindow(PlutoContext plutoContext)
        {
            InitializeComponent();

            this._plutoContext = plutoContext;
            IUnitOfWork unitOfWork = new UnitOfWork(_plutoContext);
            IDisplayService displayService = new WpfDisplayService();
            AddRoomTypeCommand addRoomTypeCommand = new AddRoomTypeCommand(unitOfWork, displayService);
            IRoomTypeWindowViewModel roomTypeWindowViewModel = new RoomTypeWindowViewModel(this, new RoomType(), addRoomTypeCommand);
            this.ViewModel = roomTypeWindowViewModel;
            DataContext = roomTypeWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
