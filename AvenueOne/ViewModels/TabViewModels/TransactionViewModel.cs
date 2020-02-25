using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.BookingCommands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
                                                            ShowDialogWindowCommand showDialogWindowCommand,
                                                            IBookingViewModel bookingViewModel,
                                                            CustomerWindow customerWindow)
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
                showDialogWindowCommand.ViewModel = this;
                showDialogWindowCommand.Window = customerWindow;
                this.AddBookingCommand = addBookingCommand;
                this.RemoveBookingCommand = removeBookingCommand;
                this.GetAvailableRoomsInTransactionCommand = getAvailableRoomsCommand;
                this.OpenCustomerWindowCommand = showDialogWindowCommand;

                CurrentDateViewed = DateTime.Today;
                this.BookingViewModel.RoomList.CollectionChanged += OnCollectionChanged;
                this.BookingViewModel.ModelList.CollectionChanged += OnCollectionChanged;

                List<Room> rooms = this.BookingViewModel.RoomList.ToList();
                OccupancyList = GenerateOccupancyList(rooms, CurrentDateViewed);


        }
        #endregion

        #region Properties

        private DateTime _currentDateViewed;

        public DateTime CurrentDateViewed
        {
            get { return _currentDateViewed; }
            set { _currentDateViewed = value;
                if(value != null)
                {
                    if(value.Year != _currentDateViewed.Year || value.Month != _currentDateViewed.Month)
                    {
                        List<Room> rooms = this.BookingViewModel.RoomList.ToList();
                        OccupancyList = GenerateOccupancyList(rooms, CurrentDateViewed);
                    }
                }

                OnPropertyChanged();
            }
        }

        //public Tuple<List<Room>, Dictionary<string, List<bool>>> OccupancyList
        private Dictionary<Room, List<Occupancy>> _occupancyList;
        public Dictionary<Room, List<Occupancy>> OccupancyList
        {
            get
            {
                return _occupancyList;
            }
            set
            {
                _occupancyList = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetAvailableRoomsInTransactionCommand { get; set; }
            public ICommand AddBookingCommand { get; set; }
            public ICommand RemoveBookingCommand { get; set; }
            public ICommand OpenCustomerWindowCommand { get; set; }

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
        public override void OnPropertyChanged([CallerMemberName] string property = "")
        {
            base.OnPropertyChanged(property);

            if(property == nameof(CurrentDateViewed))
            {
                List<Room> rooms = this.BookingViewModel.RoomList.ToList();
                OccupancyList = GenerateOccupancyList(rooms, CurrentDateViewed);
            }
        }

        public Dictionary<Room, List<Occupancy>> GenerateOccupancyList(List<Room> rooms, DateTime currentDate)
        {
            Dictionary<Room, List<Occupancy>> occupancyList = new Dictionary<Room, List<Occupancy>>();
            foreach (var room in rooms)
            {
                if (!occupancyList.ContainsKey(room))
                {
                    List<DateTime> getDatesInAMonth = GetDatesInAMonth(currentDate);
                    List<Occupancy> availabilityList = new List<Occupancy>();

                    foreach (var date in getDatesInAMonth)
                    {
                        var occupancy = new Occupancy(date, room);
                        //var occupancy = new Occupancy(date, room.GetRoomStatus(date));
                        availabilityList.Add(occupancy);
                    }
                    occupancyList.Add(room, availabilityList);
                }
            }

            return occupancyList;
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<Room> rooms = this.BookingViewModel.RoomList.ToList();
            OccupancyList = GenerateOccupancyList(rooms, CurrentDateViewed);

            //if (e.NewItems != null)
            //{
            //    foreach (var item in e.NewItems)
            //    {
            //        Debug.WriteLine(item);
            //    }
            //}

            //if (e.OldItems != null)
            //{
            //    foreach (var item in e.OldItems)
            //    {
            //        Debug.WriteLine(item);
            //    }
            //}
        }

        private List<DateTime> GetDatesInAMonth(DateTime date)
        {
            List<DateTime> datesInAMonth = new List<DateTime>();
            int year = date.Year;
            int month = date.Month;
            int daysInAMonth = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= daysInAMonth; i++)
            {
                datesInAMonth.Add(new DateTime(year, month, i));
            }

            return datesInAMonth;
        }
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
