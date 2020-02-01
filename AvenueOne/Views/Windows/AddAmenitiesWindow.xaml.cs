using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.RoomCommands;
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
    /// Interaction logic for AmenitiesWindow.xaml
    /// </summary>
    public partial class AmenitiesWindow : Window
    {
        public IObservableWindowViewModel<Amenities> ViewModel { get; set; }
        private PlutoContext _plutoContext;
        public AmenitiesWindow(PlutoContext plutoContext)
        {
            InitializeComponent();

            this._plutoContext = plutoContext;
            //AddAmenitiesCommand addAmenitiesCommand = new AddAmenitiesCommand(new UnitOfWork(plutoContext), new WpfDisplayService());
            //IAmenitiesWindowViewModel amenititesWindowViewModel = new AmenitiesWindowViewModel(this, new Amenities(), addAmenitiesCommand);
            IGenericUnitOfWork<Amenities> genericUnitOfWork = new GenericUnitOfWork<Amenities>(_plutoContext);
            IDisplayService displayService = new WpfDisplayService();
            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            BaseClassCommand<Amenities> createClassCommand = new CreateClassCommand<Amenities>(genericUnitOfWork, displayService);
            BaseClassCommand<Amenities> updateClassCommand = new UpdateClassCommand<Amenities>(genericUnitOfWork, displayService);
            BaseClassCommand<Amenities> deleteClassCommand = new DeleteClassCommand<Amenities>(genericUnitOfWork, displayService);
            IObservableWindowViewModel<Amenities> amenitiesWindowViewModel = new ObservableWindowViewModel<Amenities>(this,
                                                                                                                                                                            new Amenities(),
                                                                                                                                                                            _plutoContext.Amenities.Local,
                                                                                                                                                                            closeWindowCommand,
                                                                                                                                                                            createClassCommand,
                                                                                                                                                                            updateClassCommand,
                                                                                                                                                                            deleteClassCommand);
            this.ViewModel = amenitiesWindowViewModel;
            this.DataContext = amenitiesWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
