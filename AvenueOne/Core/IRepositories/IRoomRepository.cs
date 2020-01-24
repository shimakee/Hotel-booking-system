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
    public interface IRoomRepository : IRepository<Room>
    {
        PlutoContext PlutoContext { get; }
    }
}
