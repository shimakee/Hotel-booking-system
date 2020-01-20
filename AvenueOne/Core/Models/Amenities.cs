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
    
    public class Amenities : BaseObservableModel, IAmenities
    {
        #region Properties
        private string _id;

        [Required(ErrorMessage ="Id is required.")]
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

            public IList<RoomType> RoomTypes { get; set; }
        #endregion

        #region Constructors
            public Amenities()
            {
                this.Id = GenerateId();
                this.RoomTypes = new List<RoomType>();
            }
            public Amenities(string name)
                :this()
            {
                this.Name = name;
            }

            public Amenities(string name, IList<RoomType> roomTypes)
                :this(name)
            {
                this.RoomTypes = roomTypes;
            }
        #endregion

        #region Methods and Utilities
        public IAmenities CopyPropertyValues()
        {
            return CopyPropertyValuesTo(new Amenities());
        }
        public IAmenities CopyPropertyValuesTo(IAmenities amenities)
        {
            List<PropertyInfo> propertyList = typeof(IAmenities).GetProperties().Where(u => u.CanWrite && u.CanRead).ToList();

            foreach (PropertyInfo info in propertyList)
            {
                if (typeof(IAmenities).GetProperty(info.Name) != null)
                    info.SetValue(amenities, info.GetValue(this));
            }
            return amenities;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Amenities))
                return false;

            Amenities amenities = (Amenities)obj;

            return this.Name.ToLower() == amenities.Name.ToLower();
        }

        public override int GetHashCode()
        {
            return
                this.Name.GetHashCode();
        }
        #endregion
    }
}
