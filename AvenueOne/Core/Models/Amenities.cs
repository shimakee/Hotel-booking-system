using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class Amenities : BaseObservableModel, IAmenities
    {
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

        public IList<RoomType> RoomTypes { get; set; }

        public Amenities(string name)
        {
            this.Name = name;
        }
    }
}
