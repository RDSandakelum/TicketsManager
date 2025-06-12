using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketsManager.DataAccess.Entity;
using TicketsManager.DataAccess.TicketManagerDbContext;
using TicketsManager.Common.Models;

namespace TicketsManager.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbContext DbContext { get; set; }
        private DbSet<UserEntity> userTable {  get; set; }
        public UserRepository(TicketsManagerDbContext dbContext) 
        {
            DbContext = dbContext;
            userTable = DbContext.Set<UserEntity>();
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await userTable.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                return null;

            return new User()
            {
                id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                HashedPassword = user.HashedPassword
            };
        }

        public async Task<User> SaveUser(User user)
        {
            var userEntity = new UserEntity() 
            {
                UserName = user.UserName,
                HashedPassword = user.HashedPassword,
                Name = user.Name
            };
            await userTable.AddAsync(userEntity);
            await DbContext.SaveChangesAsync();
            return new User()
            {
                id = userEntity.Id,
                UserName = userEntity.UserName,
                Name = userEntity.Name,
                HashedPassword = userEntity.HashedPassword
            };
        }
    }
}
