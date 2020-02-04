using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.RoomCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
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
    public class RoomTypeViewModel : BaseObservableViewModel<RoomType>, IRoomTypeViewModel, INotifyPropertyChanged
    {
        public LinkAmenityCommand LinkAmenityCommand { get; set; }
        public DetachAmenityCommand DetachAmenityCommand { get; set; }
        public ObservableCollection<RoomType> RoomTypesList { get; set; }

        private IBaseObservableModel<Amenities> _amenitiesSelected; // turn to baseobservableModel
        public IBaseObservableModel<Amenities> AmenitiesSelected
        {
            get { return _amenitiesSelected; }
            set { _amenitiesSelected = value;
                OnPropertyChanged();
            }
        }
        public RoomTypeViewModel(IRoomType model,
                                                    ObservableCollection<RoomType> modelList,
                                                    BaseClassCommand<RoomType> createClassCommand,
                                                    BaseClassCommand<RoomType> updateClassCommand,
                                                    BaseClassCommand<RoomType> deleteClassCommand,
                                                    ClearClassCommand<RoomType> clearClassCommand,
                                                    LinkAmenityCommand linkAmenityCommand,
                                                    DetachAmenityCommand detachAmenityCommand)
            : base(model, modelList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.LinkAmenityCommand = linkAmenityCommand;
            this.DetachAmenityCommand = detachAmenityCommand;
            this.LinkAmenityCommand.ViewModel = this;
            this.DetachAmenityCommand.ViewModel = this;
        }
    }
}
