using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.IRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
