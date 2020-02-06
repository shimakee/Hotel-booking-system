using AvenueOne.Interfaces;
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
    public class CustomerViewModel : BaseObservableViewModel<Customer>, ICustomerViewModel
    {
        public CustomerViewModel(IPerson profile, Customer customer, ObservableCollection<Customer> CustomerList, 
            BaseClassCommand<Customer> createClassCommand, BaseClassCommand<Customer> updateClassCommand,  BaseClassCommand<Customer> deleteClassCommand,
            ClearClassCommand<Customer> clearClassCommand)
            : base(customer, CustomerList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.Profile = profile;
        }
        private Customer _modelSelected;
        public override Customer ModelSelected
        {
            get { return _modelSelected; }
            set
            {
                Model = value;
                if (value != null)
                {
                    _modelSelected = value.Copy();
                    if(value.Person != null)
                        Profile = value.Person.Copy();
                }
                    
            }
        }

        public IPerson Profile
        {
            get { return _modelSelected.Person; }
            set
            {
                _modelSelected.Person = value as Person;
                value.Customer = _modelSelected; // correct reference for when inserting
                OnPropertyChanged();
            }
        }
    }
}
