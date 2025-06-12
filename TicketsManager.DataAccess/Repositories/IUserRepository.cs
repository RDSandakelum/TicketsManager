using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Common.Models;
using TicketsManager.DataAccess.Entity;

namespace TicketsManager.DataAccess.Repositories
{
    public interface IUserRepository
    {
        public Task<User> SaveUser(User user);
        public Task<User> GetUserByUserName(string userName);
    }
}
