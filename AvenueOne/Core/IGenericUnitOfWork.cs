using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core
{
    public interface IGenericUnitOfWork<T> : IDisposable where T : class
    {
        Repository<T> Repository { get; }
        Dictionary<Type, IRepository<T>> Repositories { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
