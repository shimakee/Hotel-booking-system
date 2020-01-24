using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public RoomType RoomType { get; set; }

        #region Constructors
            public Room()
                : base()
            {
                this.Id = GenerateId();
            }

        public Room(string name)
            :this()
        {
            this.Name = name;
        }

        public Room(string name, int maxOccupants)
            :this(name)
        {
            this.MaxOccupants = maxOccupants;
        }

        public Room(string name, int maxOccupants, int floor)
            :this(name, maxOccupants)
        {
            this.Floor = floor;
        }
        #endregion


        #region Methods and Utilities
        public IRoom CopyPropertyValues()
        {
            return CopyPropertyValuesTo(new Room());
        }
        public IRoom CopyPropertyValuesTo(IRoom room)
        {
            List<PropertyInfo> propertyList = typeof(IRoom).GetProperties().Where(u => u.CanWrite && u.CanRead).ToList();

            foreach (PropertyInfo info in propertyList)
            {
                if (typeof(IRoom).GetProperty(info.Name) != null)
                    info.SetValue(room, info.GetValue(this));
            }
            return room;
        }
        #endregion
    }
}
