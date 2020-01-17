using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class OpenAmenitiesWindowCommand : ICommand
    {

        public IRoomTabViewModel ViewModel { get; set; }
        private PlutoContext _plutoContext;

        public OpenAmenitiesWindowCommand(PlutoContext plutoContext)
        {
            this._plutoContext = plutoContext ?? throw new ArgumentNullException("Pluto context cannot be null.");
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AmenitiesWindow amenitiesWindow = new AmenitiesWindow(_plutoContext);
            amenitiesWindow.ShowDialog();
        }
    }
}
