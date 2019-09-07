using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository: IRepository<User>
    {
        IEnumerable<User> GetTopUsers(int count);
        IEnumerable<User> GetUsersWithPerson(int pageIndex, int pageSize);
    }
}
