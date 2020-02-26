using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Persistence
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class, IBaseObservableModel<T>, new()
    {
        private PlutoContext _context;

        //public IRepository<T> GetRepository(Type type)
        //{
        //    var repository = Repositories[type as Type];
        //    var y = typeof(Repository<>).MakeGenericType(type);
        //    var z = Activator.CreateInstance(y);
        //    if (repository == null)
        //        Repositories.Add(type as Type, new Repository<>(_context) as IRepository<T>);
        //}
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
        //public void Rollback()
        //{
        //    _context
        //    .ChangeTracker
        //    .Entries()
        //    .ToList()
        //    .ForEach(x => x.Reload());
        //}
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
