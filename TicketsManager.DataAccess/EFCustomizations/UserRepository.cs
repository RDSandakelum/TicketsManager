using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Entity;
using TicketsManager.Common.Repository;

namespace TicketsManager.DataAccess.EFCustomizations;
public partial class TicketsManagerDbContext : IUserRepository
{
    public async Task CreateUser(UserEntity user)
    {
        var newUserEntry = await Users.AddAsync(user);
    }

    public async Task<UserEntity?> GetUserByEmail(string email)
    { 
        return await Users
            .FirstOrDefaultAsync(u => u.NormalizedEmail.Equals(email.ToUpperInvariant()));
    }

    public async Task<UserEntity> GetUserById(Guid id)
    {
        return await Users.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public Task<UserEntity?> GetUserByUsername(string username)
    {
        return Users
            .FirstOrDefaultAsync(u => u.NormalizedUsername.Equals(username.ToLower()));
    }

}
