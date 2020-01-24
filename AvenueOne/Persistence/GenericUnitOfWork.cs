using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Persistence
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T: class
    {
        private PlutoContext _context;
        public Dictionary<Type, IRepository<T>> Repositories { get; private set; } = new Dictionary<Type, IRepository<T>>();
        public GenericUnitOfWork(PlutoContext context)
        {
            this._context = context;
            this.Repositories.Add(typeof(T), new Repository<T>(_context));
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
