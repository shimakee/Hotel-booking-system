using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class AmenitiesViewModel : AccountViewModel, IAmenitiesViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<Amenities> AmenitiesList { get; set; }
        public OpenAmenitiesWindowCommand OpenAmenitiesWindowCommand { get; set; }

        public RemoveAmenitiesCommand RemoveAmenitiesCommand { get; set; }

        public EditAmenitiesCommand EditAmenitiesCommand { get; set; }

        public IAmenities _amenities;
        public IAmenities Amenities
        {
            get { return _amenities; }
            set { _amenities = value;
                OnPropertyChanged();
            }
        }
        private IAmenities _amenitiesSelected;

        public IAmenities AmenitiesSelected
        {
            get { return _amenitiesSelected; }
            set {
                Amenities = value;
                if(value != null)
                _amenitiesSelected = value.DeepCopy();
                OnPropertyChanged();
            }
        }


        public AmenitiesViewModel(OpenAmenitiesWindowCommand openAmenitiesWindowCommand,
                                                            EditAmenitiesCommand editAmenitiesCommand, RemoveAmenitiesCommand removeAmenitiesCommand,
                                                            ObservableCollection<Amenities> amenitiesList)
        {
            //this.Amenities = amenities;
            this.OpenAmenitiesWindowCommand = openAmenitiesWindowCommand;
            this.EditAmenitiesCommand = editAmenitiesCommand;
            this.RemoveAmenitiesCommand = removeAmenitiesCommand;
            this.OpenAmenitiesWindowCommand.ViewModel = this;
            this.EditAmenitiesCommand.ViewModel = this;
            this.RemoveAmenitiesCommand.ViewModel = this;
            this.AmenitiesList = amenitiesList;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
