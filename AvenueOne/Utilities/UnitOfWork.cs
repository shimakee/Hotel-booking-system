using AvenueOne.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class UnitOfWork : IUnitOfWork
    {

        //context here : TODO;

        public UnitOfWork()
        {
            Users = new UserRepository();
        }

        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
