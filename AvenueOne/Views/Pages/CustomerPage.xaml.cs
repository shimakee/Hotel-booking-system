﻿using AvenueOne.Persistence.Repositories;
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
using AvenueOne.Models;
using AvenueOne.Persistence;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.CustomerCommands;
using AvenueOne.Services.Interfaces;
using AvenueOne.Core;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.Services;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private PlutoContext _context;
        public CustomerPage(PlutoContext context)
        {
            InitializeComponent();

            this._context = context;

            Customer Customer = new Customer();
            Customer.Person = new Person();
            IGenericUnitOfWork<Customer> genericUnitOfWorkCustomer = new GenericUnitOfWork<Customer>(context);
            IDisplayService displayService = new WpfDisplayService();
            BaseClassCommand<Customer> createCustomerCommand = new CreateClassCommand<Customer>(genericUnitOfWorkCustomer, displayService);
            BaseClassCommand<Customer> updateCustomerCommand = new UpdateCustomerCommand(genericUnitOfWorkCustomer, displayService);
            BaseClassCommand<Customer> deleteCustomerCommand = new DeleteClassCommand<Customer>(genericUnitOfWorkCustomer, displayService);
            ClearClassCommand<Customer> clearCustomerCommand = new ClearCustomerCommand();
            ICustomerViewModel customerTab = new CustomerViewModel(new Person(), Customer, _context.Customers.Local,
                                                                                                                        createCustomerCommand,
                                                                                                                        updateCustomerCommand,
                                                                                                                        deleteCustomerCommand,
                                                                                                                        clearCustomerCommand);

            this.DataContext = customerTab;
        }
    }
}