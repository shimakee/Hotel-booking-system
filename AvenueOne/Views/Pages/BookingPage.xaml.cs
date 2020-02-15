using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.BookingCommands;
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
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
        private PlutoContext _context;
        public BookingPage(PlutoContext context)
        {
            InitializeComponent();

            this._context = context;

            IDisplayService displayService = new WpfDisplayService();
            IGenericUnitOfWork<Booking> genericUnitOfWorkBooking = new GenericUnitOfWork<Booking>(context);

            #region BookingTab
            BaseClassCommand<Booking> createBookingCommand = new CreateBookingCommand(genericUnitOfWorkBooking, displayService);
            BaseClassCommand<Booking> updateBookingCommand = new UpdateClassCommand<Booking>(genericUnitOfWorkBooking, displayService);
            BaseClassCommand<Booking> deleteBookingCommand = new DeleteClassCommand<Booking>(genericUnitOfWorkBooking, displayService);
            ClearClassCommand<Booking> clearBookingCommand = new ClearClassCommand<Booking>();
            GetAvailableRoomsCommand getAvailableRooms = new GetAvailableRoomsCommand();
            IBookingViewModel bookingTab = new BookingViewModel(new Booking(), _context.Bookings.Local, context.Room.Local, _context.RoomType.Local,
                                                                                                                createBookingCommand,
                                                                                                                updateBookingCommand,
                                                                                                                deleteBookingCommand,
                                                                                                                clearBookingCommand,
                                                                                                                getAvailableRooms);

            this.DataContext = bookingTab;

            #endregion
        }
    }
}
