﻿using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class CustomerTabViewModel : ICustomerTabViewModel, INotifyPropertyChanged
    {
        //todo

        #region Commands

        //public RegisterUserCommand RegisterUserCommand { get; private set; }
        public EditCustomerCommand EditCustomerCommand { get; set; }
        public OpenCustomerWindowCommand OpeCustomerWindowCommand { get; set; }
        //public RemoveUserCommand RemoveUserCommand { get; set; }

        #endregion

        #region Properties
        private ICustomer _customer;
        private IPerson _customerProfile;


        public ObservableCollection<Customer> CustomerList { get; set; }
        public IPerson CustomerProfile
        {
            get { return _customerProfile; }
            set { _customerProfile = value;
                OnPropertyChanged();
            }
        }

        public ICustomer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                CustomerProfile = value.Person.CopyPropertyValues();
                OnPropertyChanged();
            }
        }



        #endregion

        #region Constructors

        //public CustomerTabViewModel(IPerson person, ICustomer customer, ObservableCollection<ICustomer> customersList)
        public CustomerTabViewModel(IPerson person, ICustomer customer, ObservableCollection<Customer> customerList, EditCustomerCommand editCustomerCommand, OpenCustomerWindowCommand openCustomerWindowCommand)
        {
            this.Customer = customer;
            this.CustomerProfile = person;
            this.CustomerList = customerList;
            this.EditCustomerCommand = editCustomerCommand;
            this.EditCustomerCommand.ViewModel = this;
            this.OpeCustomerWindowCommand = openCustomerWindowCommand;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


    }
}