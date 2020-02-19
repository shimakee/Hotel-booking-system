using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class Transaction : BaseObservableModel<Transaction>, ITransaction
    {
        #region Constructor

            public Transaction()
                :base()
            {
                this.Bookings = new ObservableCollection<Booking>();
                this.Status = TransactionStatus.open;
            }

        public Transaction(User employee, Customer customer)
            :this()
        {
            this.Employee = employee;
            this.Customer = customer;
        }

        public Transaction(User employee, Customer customer, ObservableCollection<Booking> bookings)
            :this(employee, customer)
        {
            this.Bookings = bookings;
        }
        #endregion

        #region Properties
        private TransactionStatus _status;
        public TransactionStatus Status
        {
            get { return _status; }
            set { _status = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Booking> _bookings;
        public ObservableCollection<Booking> Bookings
        {
            get { return _bookings; }
            set { _bookings = value;
                OnPropertyChanged();
            }
        }


        private Customer _customer;
        [Required(ErrorMessage = "A customer must be selected.")]
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value;
                OnPropertyChanged();
            }
        }


        private User _employee;
        public User Employee
        {
            get { return _employee; }
            set { _employee = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
