using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.IRepositories
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        PlutoContext PlutoContext { get; }
    }
}
