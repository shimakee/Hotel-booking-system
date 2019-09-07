using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public PlutoContext PlutoContext { get { return Context as PlutoContext; } }

        public UserRepository(PlutoContext context) 
            : base(context)
        {
        }

        public IEnumerable<User> GetTopUsers(int count)
        {
            return PlutoContext.Users.OrderByDescending(u => u.Username).Take(count).ToList();
            //return Context.Set<IUser>().Where(u => u.Username.Length < 20);
        }

        public IEnumerable<User> GetUsersWithPerson(int pageIndex, int pageSize)
        {
            //return PlutoContext.Users.Include(u => u.Person)
            //                                .OrderBy(p => p.LastName)
            //                                .Skip((pageIndex - 1) * pageSize)
            //                                .Take(pageSize)
            //                                .ToList();
            throw new NotImplementedException("no method yet");
        }


    }
}
