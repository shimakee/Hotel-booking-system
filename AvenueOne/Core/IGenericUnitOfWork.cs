using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core
{
    public interface IGenericUnitOfWork<T> : IDisposable where T : class
    {
        Dictionary<Type, IRepository<T>> Repositories { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
