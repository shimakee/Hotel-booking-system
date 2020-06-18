using AvenueOne.Core.Models;
using AvenueOne.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
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
    /// Interaction logic for AmenityWindow.xaml
    /// </summary>
    public partial class AmenityWindow : Window
    {
        private PlutoContext _context;
        public AmenityWindow(PlutoContext context)
        {

            if (context == null)
                throw new ArgumentNullException("Context cannot be null.");

            InitializeComponent();

            this._context = context;
            User User = new User();
            User.Person = new Person();

            IDisplayService displayService = new WpfDisplayService();


            InitializeComponent();

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

            DataContext = amenitiesViewModel;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
