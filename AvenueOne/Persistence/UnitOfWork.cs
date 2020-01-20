using AvenueOne.Core.IRepositories;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlutoContext _context;
        public IUserRepository Users { get; private set; }
        public IPersonRepository People { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IAmenitiesRepository Amenities { get; private set; }

        public UnitOfWork(PlutoContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            People = new PersonRepository(_context);
            Customers = new CustomerRepository(_context);
            Amenities = new AmenitiesRepository(_context);
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
