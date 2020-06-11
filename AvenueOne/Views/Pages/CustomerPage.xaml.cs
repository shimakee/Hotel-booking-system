using AvenueOne.Persistence.Repositories;
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
using AvenueOne.Views.Windows;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public Window Window { get; }
        private PlutoContext _context;
        private ICustomerViewModel _viewModel;
        public CustomerPage(PlutoContext context, Window window)
        {
            if (context == null)
                throw new ArgumentNullException("Context cannot be null.");
            if (window == null)
                throw new ArgumentNullException("Window page caller must not be null.");

            this._context = context;
            this.Window = window;


            InitializeComponent();

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
            this._viewModel = customerTab;
            this.DataContext = customerTab;
        }

        private void Button_ChangeVisibility(object sender, RoutedEventArgs e)
        {
            if (_viewModel.UserAccount.IsAdmin)
            {
                if(Card.Visibility == Visibility.Visible)
                {
                    //SaveEditButton.Visibility = Visibility.Visible;
                    Form.Visibility = Visibility.Visible;
                    Card.Visibility = Visibility.Collapsed;
                    EditButton.Content = "Close";
                }
                else
                {
                    //SaveEditButton.Visibility = Visibility.Collapsed;
                    Form.Visibility = Visibility.Collapsed;
                    Card.Visibility = Visibility.Visible;
                    EditButton.Content = "Edit";
                }
            }
        }

        private void Button_OpenCustomerWindow(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow(_context);
            customerWindow.Owner = Window.GetWindow(this);
            customerWindow.ShowDialog();
        }
    }
}
