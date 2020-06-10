using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.BookingCommands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Windows;
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
using System.Data.Entity;
using AvenueOne.ViewModels.Commands.TransactionCommands;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {

        public Window Window { get; }
        private PlutoContext _context;
        private ITransactionViewModel _viewModel;
        public BookingPage(PlutoContext context, Window window)
        {
            if (context == null)
                throw new ArgumentNullException("Context cannot be null.");
            if (window == null)
                throw new ArgumentNullException("Window page caller must not be null.");

            this._context = context;
            this.Window = window;

            InitializeComponent();

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

            #endregion


            #region TransactionTab

            IGenericUnitOfWork<Transaction> genericUnitOfWorkTransaction = new GenericUnitOfWork<Transaction>(context);
            BaseClassCommand<Transaction> createTransactionCommand = new CreateClassCommand<Transaction>(genericUnitOfWorkTransaction, displayService);
            BaseClassCommand<Transaction> updateTransactionCommand = new UpdateTransactionCommand(genericUnitOfWorkTransaction, genericUnitOfWorkBooking, displayService);
            BaseClassCommand<Transaction> deleteTransactionCommand = new DeleteTransactionCommand(genericUnitOfWorkTransaction, genericUnitOfWorkBooking, displayService);
            ClearClassCommand<Transaction> clearTransactionCommand = new ClearTransactionCommand();
            AddBookingCommand addBookingCommand = new AddBookingCommand(genericUnitOfWorkTransaction, displayService);
            RemoveBookingCommand removeBookingCommand = new RemoveBookingCommand(genericUnitOfWorkTransaction, displayService);
            GetAvailableRoomsInTransactionCommand getAvailableRoomsCommand = new GetAvailableRoomsInTransactionCommand(displayService);
            ShowDialogWindowCommand showDialogWindowCommand = new ShowDialogWindowCommand();
            CustomerWindow customerWindow = new CustomerWindow(context);
            ITransactionViewModel transactionTab = new TransactionViewModel(new Transaction(), _context.Transactions.Local,
                                                                                                                            _context.Customers.Local,
                                                                                                                            _context.Users.Local,
                                                                                                                            createTransactionCommand,
                                                                                                                            updateTransactionCommand,
                                                                                                                            deleteTransactionCommand,
                                                                                                                            clearTransactionCommand,
                                                                                                                            addBookingCommand,
                                                                                                                            removeBookingCommand,
                                                                                                                            getAvailableRoomsCommand,
                                                                                                                            showDialogWindowCommand,
                                                                                                                            bookingTab,
                                                                                                                            customerWindow);
            #endregion
            this.DataContext = transactionTab;
            this._viewModel = transactionTab;
        }

        private void Button_AddMonth(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentDateViewed = _viewModel.CurrentDateViewed.AddMonths(1);
        }
        private void Button_LessMonth(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentDateViewed = _viewModel.CurrentDateViewed.AddMonths(-1);
        }
        private void Button_AddYear(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentDateViewed = _viewModel.CurrentDateViewed.AddYears(1);
        }
        private void Button_LessYear(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentDateViewed = _viewModel.CurrentDateViewed.AddYears(-1);
        }
    }
}
