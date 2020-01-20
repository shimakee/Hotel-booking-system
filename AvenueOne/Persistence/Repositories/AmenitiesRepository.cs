using AvenueOne.Core.IRepositories;
using AvenueOne.Core.Models;
using AvenueOne.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Persistence.Repositories
{
    public class AmenitiesRepository : Repository<Amenities> ,IAmenitiesRepository
    {
        public PlutoContext PlutoContext { get { return Context as PlutoContext; } }
        public AmenitiesRepository(PlutoContext plutoContext)
            : base(plutoContext)
        {

        }
    }
}
