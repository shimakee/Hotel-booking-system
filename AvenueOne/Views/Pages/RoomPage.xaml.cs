using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.TabViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {
        public Window Window { get; }
        private PlutoContext _context;
        public RoomPage(PlutoContext context, Window window)
        {
            if (context == null)
                throw new ArgumentNullException("Context cannot be null.");
            if (window == null)
                throw new ArgumentNullException("Window page caller must not be null.");

            this._context = context;
            this.Window = window;

            InitializeComponent();

            IGenericUnitOfWork<Room> genericUnitOfWorkRoom = new GenericUnitOfWork<Room>(context);
            IDisplayService displayService = new WpfDisplayService();
            BaseClassCommand<Room> createRoomCommand = new CreateClassCommand<Room>(genericUnitOfWorkRoom, displayService);
            BaseClassCommand<Room> updateRoomCommand = new UpdateClassCommand<Room>(genericUnitOfWorkRoom, displayService);
            BaseClassCommand<Room> deleteRoomCommand = new DeleteClassCommand<Room>(genericUnitOfWorkRoom, displayService);
            ClearClassCommand<Room> clearRoomCommand = new ClearClassCommand<Room>();
            IRoomViewModel roomViewModel = new RoomViewModel(new Room(),
                                                                                                            context.Room.Local,
                                                                                                            context.RoomType.Local,
                                                                                                            createRoomCommand,
                                                                                                            updateRoomCommand,
                                                                                                            deleteRoomCommand,
                                                                                                            clearRoomCommand);

            this.DataContext = roomViewModel;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
