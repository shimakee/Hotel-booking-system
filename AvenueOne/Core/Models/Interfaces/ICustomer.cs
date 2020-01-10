using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface ICustomer
    {
        string Id { get; set; }
        Person Person { get; set; }
    }
}
