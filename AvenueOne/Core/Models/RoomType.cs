using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage ="Name is required.")]
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

        private decimal _rate;
        [Required(ErrorMessage ="Rate is required.")]
        public decimal Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                OnPropertyChanged();
            }
        }

        private rateType _rateType;
        public rateType RateType
        {
            get { return _rateType; }
            set
            {
                _rateType = value;
                OnPropertyChanged();
            }
        }

        public IList<Amenities> Amenities { get; set; }
        public IList<Room> Rooms { get; set; }

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

        public RoomType(string name, decimal rate, rateType rateType)
            :this()
        {
            this.Name = name;
            this.Rate = rate;
            this.RateType = rateType;
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

                if(!String.IsNullOrWhiteSpace(this.Name) && !String.IsNullOrWhiteSpace(roomType.Name))
                    return this.Name.ToLower() == roomType.Name.ToLower();
                if (String.IsNullOrWhiteSpace(this.Name) && String.IsNullOrWhiteSpace(roomType.Name))
                    return true;
            return false;
            }

            public override int GetHashCode()
            {
            return
                this.Id.GetHashCode();
            }
        #endregion
    }
}
