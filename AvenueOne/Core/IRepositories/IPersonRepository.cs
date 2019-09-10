using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.IRepositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        //PlutoContext PlutoContext { get; }
    }
}
