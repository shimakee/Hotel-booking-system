﻿using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
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

            }
        #endregion

        #region Properties
        private List<Booking> _bookings;

        public List<Booking> Bookings
        {
            get { return _bookings; }
            set { _bookings = value; }
        }

        private Customer _customer;

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