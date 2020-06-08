using AvenueOne.Core;
using AvenueOne.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.CustomerCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
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
using System.Windows.Shapes;

namespace AvenueOne.Views.Windows
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private PlutoContext _context;
        public CustomerWindow(PlutoContext context)
        {
            InitializeComponent();

            this._context = context;
            Customer customer = new Customer();
            customer.Person = new Person();
            IDisplayService displayService = new WpfDisplayService();
            IGenericUnitOfWork<Customer> genericUnitOfWork = new GenericUnitOfWork<Customer>(_context);
            BaseClassCommand<Customer> createCustomerCommand = new CreateClassCommand<Customer>(genericUnitOfWork, displayService);
            BaseClassCommand<Customer> updateCustomerCommand = new UpdateCustomerCommand(genericUnitOfWork, displayService);
            BaseClassCommand<Customer> deleteCustomerCommand = new DeleteClassCommand<Customer>(genericUnitOfWork, displayService);
            ClearClassCommand<Customer> clearCustomerCommand = new ClearCustomerCommand();

            ICustomerViewModel customerViewModel = new CustomerViewModel(new Person(), customer, _context.Customers.Local,
                                                                                                                                createCustomerCommand,
                                                                                                                                updateCustomerCommand,
                                                                                                                                deleteCustomerCommand,
                                                                                                                                clearCustomerCommand);
            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            CustomerWindowViewModel customerWindowViewModel = new CustomerWindowViewModel(this, closeWindowCommand, customerViewModel);


            DataContext = customerWindowViewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}
