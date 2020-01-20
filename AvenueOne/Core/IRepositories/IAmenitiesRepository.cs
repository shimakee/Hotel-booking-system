using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.IRepositories
{
    public interface IAmenitiesRepository : IRepository<Amenities>
    {
        PlutoContext PlutoContext { get; }
    }
}
