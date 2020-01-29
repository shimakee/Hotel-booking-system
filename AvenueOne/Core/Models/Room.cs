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
    public class Room : BaseObservableModel<Room>, IRoom
    {
        #region Properties

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
        #endregion

        #region Constructors
            public Room()
                : base()
            {
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

        #region Overrides
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                if (!(obj is Room))
                    return false;

                Room room = (Room)obj;

            //if (!String.IsNullOrWhiteSpace(this.Name) && !String.IsNullOrWhiteSpace(room.Name))
            //    return this.Name.ToLower() == room.Name.ToLower() && this.Id == room.Id;
            //if (String.IsNullOrWhiteSpace(this.Name) && String.IsNullOrWhiteSpace(room.Name))
            //    return this.Id == room.Id;
            //return false;

            return this.Id == room.Id &&
                        this.Name == room.Name &&
                        this.Floor == room.Floor &&
                        this.MaxOccupants == room.MaxOccupants &&
                        this.RoomType.Equals(room.RoomType);

            }

            public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
