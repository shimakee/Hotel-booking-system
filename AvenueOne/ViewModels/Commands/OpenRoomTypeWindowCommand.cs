using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.UserCommands
{
    public class OpenRoomTypeWindowCommand : ICommand
    {
        public IRoomTypeViewModel ViewModel { get; set; }
        private PlutoContext _plutoContext;

        public OpenRoomTypeWindowCommand(PlutoContext context)
        {
            this._plutoContext = context;
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

                AddRoomTypeWindow addRoomTypeWindow = new AddRoomTypeWindow(_plutoContext);
                addRoomTypeWindow.ShowDialog();
            
        }
    }
}
