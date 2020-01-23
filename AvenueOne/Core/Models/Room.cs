using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class Room : BaseObservableModel, IRoom
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;
                OnPropertyChanged();
            }
        }

        private int _floor;
        public int Floor
        {
            get { return _floor; }
            set { _floor = value;
                OnPropertyChanged();
            }
        }

        private int _maxOccupants;
        public int MaxOccupants
        {
            get { return _maxOccupants; }
            set { _maxOccupants = value;
                OnPropertyChanged();
            }
        }

        private RoomType _roomType;
        public RoomType RoomType
        {
            get { return _roomType; }
            set { _roomType = value;
                OnPropertyChanged();
            }
        }

        public Room()
            : base()
        {
            this.Id = GenerateId();
        }
    }
}
