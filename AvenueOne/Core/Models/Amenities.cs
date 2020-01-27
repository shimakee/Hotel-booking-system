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
    
    public class Amenities : BaseObservableModel<Amenities>, IAmenities
    {
        #region Properties

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

        #endregion
        #region Reference
        public IList<RoomType> RoomTypes { get; set; }

        #endregion

        #region Constructors
        public Amenities()
            :base()
            {
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

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Amenities))
                return false;

            Amenities amenities = (Amenities)obj;

            if (!String.IsNullOrWhiteSpace(this.Name) && !String.IsNullOrWhiteSpace(amenities.Name))
                return this.Name.ToLower() == amenities.Name.ToLower() && this.Id == amenities.Id;
            if (String.IsNullOrWhiteSpace(this.Name) && String.IsNullOrWhiteSpace(amenities.Name))
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
