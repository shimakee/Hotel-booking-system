using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands.CustomerCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
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
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        private PlutoContext _plutoContext;
        public ICustomerWindowViewModel ViewModel { get; private set; }
        public AddCustomerWindow(PlutoContext plutoContext)
        {
            InitializeComponent();


            this._plutoContext = plutoContext ?? throw new ArgumentNullException("Context cannot be null.");

            Customer customer = new Customer();
            customer.Person = new Person();
            IDisplayService displayService = new WpfDisplayService();
            IUnitOfWork unitOfWork = new UnitOfWork(_plutoContext);
            AddCustomerCommand addCustomerCommand = new AddCustomerCommand(unitOfWork, displayService);
            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            ICustomerWindowViewModel customerWindowViewModel = new CustomerWindowViewModel(this, 
                closeWindowCommand, addCustomerCommand, customer, new Person());

            this.ViewModel = customerWindowViewModel;
            DataContext = customerWindowViewModel;
        }



        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //_plutoContext.Dispose();
        }
    }
}
