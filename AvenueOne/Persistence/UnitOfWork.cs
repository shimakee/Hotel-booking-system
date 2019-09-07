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

        public UnitOfWork(PlutoContext context)
        {
            _context = context;
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
