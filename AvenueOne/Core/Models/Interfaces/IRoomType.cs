using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models.Interfaces
{
    public interface IRoomType : IBaseObservableModel
    {
        string Id { get; set; }
        string Name { get; set; }
        string Details { get; set; }
        IList<Amenities> Amenities { get; set; }
        IRoomType CopyPropertyValues();
        IRoomType CopyPropertyValuesTo(IRoomType roomType);
    }
}
