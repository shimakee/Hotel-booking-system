using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.CustomerCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class CustomerWindowViewModel : WindowViewModel, INotifyPropertyChanged, ICustomerWindowViewModel
    {
        #region Properties

        public AddCustomerCommand AddCustomerCommand { get; set; }
        private ICustomer _customer;
        private IPerson _customerProfile;
        public IPerson CustomerProfile
        {
            get { return _customerProfile; }
            set
            {
                _customerProfile = value;
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

            public CustomerWindowViewModel(Window window, AddCustomerCommand addCustomerCommand, ICustomer customer, IPerson person)
                :base(window)
            {
                this.Customer = customer;
                this.CustomerProfile = person;
                this.AddCustomerCommand = addCustomerCommand;
                this.AddCustomerCommand.ViewModel = this;
            }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
