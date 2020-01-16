using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _details;
        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }

        public IList<Amenities> Amenities { get; set; }

        public RoomType(string name)
        {
            this.Name = name;
        }
    }
}
