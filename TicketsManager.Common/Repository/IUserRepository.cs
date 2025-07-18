using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Dto;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface IUserRepository
{
    DbSet<UserEntity> Users { get; set; }
    Task<UserDto> GetUserById(Guid id);
    Task<UserEntity?> GetUserByUsername(string username);
    Task<UserEntity?> GetUserByEmail(string email);
    Task CreateUser(UserEntity user);
}
