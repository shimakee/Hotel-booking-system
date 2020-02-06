using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.CustomerCommands;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.PagesViewModels;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private PlutoContext _context;
        public AdminPageViewModel ViewModel { get; private set; }
        public AdminPage(PlutoContext context)
        {
            if (context == null)
                throw new ArgumentNullException("Context cannot be null.");

            InitializeComponent();

            this._context = context;
            User User = new User();
            User.Person = new Person();
            
            IDisplayService displayService = new WpfDisplayService();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            RegisterUserCommand RegisterUserCommand = new RegisterUserCommand(context);
            EditUserProfileCommand editProfileCommand = new EditUserProfileCommand(unitOfWork, 
                                                                                                                                displayService);
            RemoveUserCommand removeUserCommand = new RemoveUserCommand(unitOfWork, 
                                                                                                                                        displayService);

            IGenericUnitOfWork<User> genericUnitOfWorkUser = new GenericUnitOfWork<User>(context);
            //BaseClassCommand<User> createUserCommand = new CreateClassCommand<User>(genericUnitOfWorkUser, displayService);
            BaseClassCommand<User> createUserCommand = new CreateUserCommand(genericUnitOfWorkUser, displayService);
            BaseClassCommand<User> updateUserCommand = new UpdateClassCommand<User>(genericUnitOfWorkUser, displayService);
            BaseClassCommand<User> deleteUserCommand = new DeleteClassCommand<User>(genericUnitOfWorkUser, displayService);
            ClearClassCommand<User> clearUserCommand = new ClearClassCommand<User>();
            IUserViewModel userTab = new UserViewModel(new Person(), User, context.Users.Local,
                                                                                                        createUserCommand,
                                                                                                        updateUserCommand,
                                                                                                        deleteUserCommand,
                                                                                                        clearUserCommand);

            #region Customer tab
                Customer Customer = new Customer();
            Customer.Person = new Person();
            //EditCustomerCommand editCustomerCommand = new EditCustomerCommand(unitOfWork, displayService);
            //OpenCustomerWindowCommand openCustomerWindowCommand = new OpenCustomerWindowCommand(context);
            //RemoveCustomerCommand removeCustomerCommand = new RemoveCustomerCommand(unitOfWork, displayService);

            //CustomerTabViewModel customerTab = new CustomerTabViewModel(new Person(), Customer, _context.Customers.Local, editCustomerCommand, openCustomerWindowCommand, removeCustomerCommand);
            IGenericUnitOfWork<Customer> genericUnitOfWorkCustomer = new GenericUnitOfWork<Customer>(context);
                //BaseClassCommand<Customer> createCustomerCommand = new AddCustomerCommand(genericUnitOfWorkCustomer, displayService);
                BaseClassCommand<Customer> createCustomerCommand = new CreateClassCommand<Customer>(genericUnitOfWorkCustomer, displayService);
                //BaseClassCommand<Customer> updateCustomerCommand = new UpdateClassCommand<Customer>(genericUnitOfWorkCustomer, displayService);
                BaseClassCommand<Customer> updateCustomerCommand = new UpdateCustomerCommand(genericUnitOfWorkCustomer, displayService);
                BaseClassCommand<Customer> deleteCustomerCommand = new DeleteClassCommand<Customer>(genericUnitOfWorkCustomer, displayService);
                ClearClassCommand<Customer> clearCustomerCommand = new ClearCustomerCommand();
                //ClearClassCommand<Customer> clearCustomerCommand = new ClearClassCommand<Customer>();
                ICustomerViewModel customerTab = new CustomerViewModel(new Person(), Customer, _context.Customers.Local,
                                                                                                                            createCustomerCommand,
                                                                                                                            updateCustomerCommand,
                                                                                                                            deleteCustomerCommand,
                                                                                                                            clearCustomerCommand);
            #endregion

            #region RoomTab
                #region Amenities view model


            GenericUnitOfWork<Amenities> genericUnitOfWorkAmenities = new GenericUnitOfWork<Amenities>(context);
            BaseClassCommand<Amenities> createAmenitiesCommand = new CreateClassCommand<Amenities>(genericUnitOfWorkAmenities, displayService);
            BaseClassCommand<Amenities> updateAmenitiesCommand = new UpdateClassCommand<Amenities>(genericUnitOfWorkAmenities, displayService);
            BaseClassCommand<Amenities> deleteAmenitiesCommand = new DeleteClassCommand<Amenities>(genericUnitOfWorkAmenities, displayService);
            ClearClassCommand<Amenities> clearAmenitiesCommand = new ClearClassCommand<Amenities>();
            IBaseObservableViewModel<Amenities> amenitiesViewModel = new BaseObservableViewModel<Amenities>(new Amenities(),
                                                                                                                                                                                            context.Amenities.Local,
                                                                                                                                                                                            createAmenitiesCommand,
                                                                                                                                                                                            updateAmenitiesCommand,
                                                                                                                                                                                            deleteAmenitiesCommand,
                                                                                                                                                                                            clearAmenitiesCommand);
                #endregion

                #region RoomType view model

                GenericUnitOfWork<RoomType> genericUnitOfWorkRoomType = new GenericUnitOfWork<RoomType>(context);
                BaseClassCommand<RoomType> createRoomTypeCommand = new CreateClassCommand<RoomType>(genericUnitOfWorkRoomType, displayService);
                BaseClassCommand<RoomType> updateRoomTypeCommand = new UpdateClassCommand<RoomType>(genericUnitOfWorkRoomType, displayService);
                BaseClassCommand<RoomType> deleteRoomTypeCommand = new DeleteClassCommand<RoomType>(genericUnitOfWorkRoomType, displayService);
                ClearClassCommand<RoomType> clearRoomTypeCommand = new ClearClassCommand<RoomType>();
                LinkAmenityCommand linkAmenityCommand = new LinkAmenityCommand(genericUnitOfWorkRoomType, displayService);
                DetachAmenityCommand detachAmenityCommand = new DetachAmenityCommand(genericUnitOfWorkRoomType, displayService);
                IRoomTypeViewModel roomTypeViewModel = new RoomTypeViewModel(new RoomType(),
                                                                                                                                        _context.RoomType.Local,
                                                                                                                                        createRoomTypeCommand,
                                                                                                                                        updateRoomTypeCommand,
                                                                                                                                        deleteRoomTypeCommand,
                                                                                                                                        clearRoomTypeCommand,
                                                                                                                                        linkAmenityCommand,
                                                                                                                                        detachAmenityCommand);
                #endregion

                #region Room view model

                IGenericUnitOfWork<Room> genericUnitOfWorkRoom = new GenericUnitOfWork<Room>(context);
                BaseClassCommand<Room> createRoomCommand = new CreateClassCommand<Room>(genericUnitOfWorkRoom, displayService);
                BaseClassCommand<Room> updateRoomCommand = new UpdateClassCommand<Room>(genericUnitOfWorkRoom, displayService);
                BaseClassCommand<Room> deleteRoomCommand = new DeleteClassCommand<Room>(genericUnitOfWorkRoom, displayService);
                ClearClassCommand<Room> clearRoomCommand = new ClearClassCommand<Room>();
                IBaseObservableViewModel<Room> roomViewModel = new BaseObservableViewModel<Room>(new Room(),
                                                                                                                                                                                context.Room.Local,
                                                                                                                                                                                createRoomCommand,
                                                                                                                                                                                updateRoomCommand,
                                                                                                                                                                                deleteRoomCommand,
                                                                                                                                                                                clearRoomCommand);
                #endregion

                RoomTabViewModel roomTab = new RoomTabViewModel(amenitiesViewModel,
                                                                                                                roomTypeViewModel,
                                                                                                                roomViewModel);
            #endregion

            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            AdminPageViewModel _adminViewModel = new AdminPageViewModel(Window.GetWindow(this), 
                                                                                                                                closeWindowCommand,
                                                                                                                                userTab,
                                                                                                                                customerTab,
                                                                                                                                roomTab);
            //AdminPageViewModel _adminViewModel = new AdminPageViewModel(Window.GetWindow(this),
            //                                                                                                                    closeWindowCommand,
            //                                                                                                                    RegisterUserCommand,
            //                                                                                                                    editProfileCommand,
            //                                                                                                                    removeUserCommand,
            //                                                                                                                    User,
            //                                                                                                                    _context.Users.Local,
            //                                                                                                                    customerTab,
            //                                                                                                                    roomTab);


            this.ViewModel = _adminViewModel;
            DataContext = _adminViewModel;
        }

        //used a bit of code behind because its easier to implement
        private void Refresh_UsersList(object sender, RoutedEventArgs e)
        {
            if (_context != null && ViewModel != null)
            {
                _context.Users.ToList();
                this.ViewModel.UsersList = _context.Users.Local;
            }
            UsersList.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var person = this.ViewModel.Person;
            //var user = this.ViewModel.User;
            //MessageBox.Show($"Username:{user.Username}\nFull name:{person.FullName}.");
            if (_context != null)
                _context.SaveChanges();
        }
    }
}
