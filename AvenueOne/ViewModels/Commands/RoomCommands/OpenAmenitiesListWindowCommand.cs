using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.RoomCommands
{
    public class OpenAmenitiesListWindowCommand :  ICommand
    {
        public IRoomTypeViewModel ViewModel { get; set; }
        private PlutoContext _plutoContext;
        public OpenAmenitiesListWindowCommand(PlutoContext plutoContext)
        {
            this._plutoContext = plutoContext;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (ViewModel != null)
                return ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public void Execute(object parameter)
        {
            try
            {
                AmenitiesListWindow amenitiesListWindow = new AmenitiesListWindow(_plutoContext);
                amenitiesListWindow.ShowDialog();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
