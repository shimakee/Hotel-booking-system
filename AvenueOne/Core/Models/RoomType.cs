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
    public class RoomType : BaseObservableModel, IRoomType
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value;
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

        private string _details;
        public string Details
        {
            get { return _details; }
            set { _details = value;
                OnPropertyChanged();
            }
        }

        public IList<Amenities> Amenities { get; set; }

        #region Constructors
            public RoomType()
            {
                this.Amenities = new List<Amenities>();
                this.Id = GenerateId();
            }
            public RoomType(string name)
            :this()
            {
                this.Name = name;
            }

            public RoomType(string name, IList<Amenities> amenities)
                :this(name)
            {
                this.Amenities = amenities;
            }
        #endregion

        #region Methods and Utilities
            public IRoomType CopyPropertyValues()
            {
                return CopyPropertyValuesTo(new RoomType());
            }
            public IRoomType CopyPropertyValuesTo(IRoomType roomType)
            {
                List<PropertyInfo> propertyList = typeof(IRoomType).GetProperties().Where(u => u.CanWrite && u.CanRead).ToList();

                foreach (PropertyInfo info in propertyList)
                {
                    if (typeof(IRoomType).GetProperty(info.Name) != null)
                        info.SetValue(roomType, info.GetValue(this));
                }
                return roomType;
            }
        #endregion

        #region Overrides
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                if (!(obj is RoomType))
                    return false;

                RoomType roomType = (RoomType)obj;

                return this.Name.ToLower() == roomType.Name.ToLower();
            }

            public override int GetHashCode()
            {
            return
                this.Name.GetHashCode();
            }
        #endregion
    }
}
