using AvenueOne.Core.Models;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class TransactionViewModel : BaseObservableViewModel<Transaction>, ITransactionViewModel
    {
        public TransactionViewModel(Transaction transaction, ObservableCollection<Transaction> transactionList,
                                                        BaseClassCommand<Transaction> createClassCommand,
                                                        BaseClassCommand<Transaction> updateClassCommand,
                                                        BaseClassCommand<Transaction> deleteClassCommand,
                                                        ClearClassCommand<Transaction> clearClassCommand,
                                                        IBookingViewModel bookingViewModel)
            :base(transaction, transactionList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.Bookings = new List<Booking>();
            this.EmployeeSelected = UserAccount as User;
            this.BookingViewModel = bookingViewModel;
            
        }

        private IBookingViewModel _bookingViewModel;

        public IBookingViewModel BookingViewModel
        {
            get { return _bookingViewModel; }
            set { _bookingViewModel = value;
                OnPropertyChanged();
            }
        }

        public List<Booking> Bookings
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

        private ObservableCollection<User> _employee;
        public ObservableCollection<User> EmployeeList
        {
            get { return _employee; }
            set { _employee = value;
                OnPropertyChanged();
            }
        }


    }
}
