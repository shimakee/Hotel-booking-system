using AvenueOne.Core.Models;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.BookingCommands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class TransactionViewModel : BaseObservableViewModel<Transaction>, ITransactionViewModel
    {
        #region Constructors

            public TransactionViewModel(Transaction transaction, ObservableCollection<Transaction> transactionList,
                                                            ObservableCollection<Customer> customerList,
                                                            ObservableCollection<User> userList,
                                                            BaseClassCommand<Transaction> createClassCommand,
                                                            BaseClassCommand<Transaction> updateClassCommand,
                                                            BaseClassCommand<Transaction> deleteClassCommand,
                                                            ClearClassCommand<Transaction> clearClassCommand,
                                                            AddBookingCommand addBookingCommand,
                                                            RemoveBookingCommand removeBookingCommand,
                                                            GetAvailableRoomsInTransactionCommand getAvailableRoomsCommand,
                                                            IBookingViewModel bookingViewModel)
                :base(transaction, transactionList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
            {
                this.Bookings = new ObservableCollection<Booking>();
                this.CustomerList = customerList;
                this.EmployeeList = userList;
                this.EmployeeSelected = EmployeeList.Where(e => e.Id == UserAccount.Id).FirstOrDefault();
                this.BookingViewModel = bookingViewModel;
                addBookingCommand.ViewModel = this;
                removeBookingCommand.ViewModel = this;
                getAvailableRoomsCommand.ViewModel = this;
                this.AddBookingCommand = addBookingCommand;
                this.RemoveBookingCommand = removeBookingCommand;
                this.GetAvailableRoomsInTransactionCommand = getAvailableRoomsCommand;
            }
        #endregion

        #region Properties

            public ICommand GetAvailableRoomsInTransactionCommand { get; set; }
            public ICommand AddBookingCommand { get; set; }
            public ICommand RemoveBookingCommand { get; set; }

            private IBookingViewModel _bookingViewModel;

            public IBookingViewModel BookingViewModel
            {
                get { return _bookingViewModel; }
                set { _bookingViewModel = value;
                    OnPropertyChanged();
                }
            }

            public ObservableCollection<Booking> Bookings
            {
                get { return ModelSelected.Bookings; }
                set { ModelSelected.Bookings = value;
                    OnPropertyChanged();
                }
            }


            public Customer CustomerSelected
            {
                get { return ModelSelected.Customer; }
                set { ModelSelected.Customer = value;
                    OnPropertyChanged();
                }
            }

            private ObservableCollection<Customer> _customerList;
            public ObservableCollection<Customer> CustomerList
            {
                get { return _customerList; }
                set { _customerList = value;
                    OnPropertyChanged();
                }
            }

            public User EmployeeSelected
            {
                get { return ModelSelected.Employee; }
                set { ModelSelected.Employee = value;
                    OnPropertyChanged();
                }
            }

            private ObservableCollection<User> _employeeList;
            public ObservableCollection<User> EmployeeList
            {
                get { return _employeeList; }
                set { _employeeList = value;
                    OnPropertyChanged();
                }
            }
        #endregion

        #region Methods

        public List<Room> GetAvailableRooms()
        {
            List<Booking> bookingList = BookingViewModel.ModelList.ToList();
            
            if(this.Bookings != null)
            {
               if(this.Bookings.Count > 0)
                {
                    foreach (var item in Bookings)
                    {
                        bookingList.Add(item);
                    }
                }
            }

            return BookingViewModel.GetAvailableRooms(bookingList);
        }

        public List<Room> GetAvailableRooms(List<Booking> bookingList, Booking currentBooking, List<Room> roomList, RoomType roomTypeSelected)
        {
            return BookingViewModel.GetAvailableRooms(bookingList, currentBooking, roomList, roomTypeSelected);
        }

        #endregion

    }
}
