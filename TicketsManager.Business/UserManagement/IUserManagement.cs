using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Common.Models;
using TicketsManager.Common.RequestDtos;
using TicketsManager.Common.ResponseDtos;

namespace TicketsManager.Business.UserManagement;

public interface IUserManagement
{
    public Result<UserResponseDto> RegisterUser(RegisterUserDto user);
    public Result<UserResponseDto> LoginUser(LoginUserDto user);
    public Result<UserResponseDto> GetUserById(Guid id);
    public Result<User> GetUserByUserName(string userName);
}
