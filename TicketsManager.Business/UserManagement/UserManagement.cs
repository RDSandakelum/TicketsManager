using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Business.SecurityService;
using TicketsManager.Common.Models;
using TicketsManager.Common.RequestDtos;
using TicketsManager.Common.ResponseDtos;
using TicketsManager.DataAccess.Repositories;

namespace TicketsManager.Business.UserManagement
{
    public class UserManagement : IUserManagement
    {
        private IUserRepository userRepository;
        private ISecurityService securityService;

        public UserManagement(IUserRepository userRepository, ISecurityService securityService)
        {
            this.userRepository = userRepository;
            this.securityService = securityService;
        }

        public Result<UserResponseDto> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<UserResponseDto> LoginUser(LoginUserDto user)
        {
            var existingUserResult = GetUserByUserName(user.Username);

            if (!existingUserResult.IsSuccess)
                return Result<UserResponseDto>.Failure("Username or password incorrect.");

            var existingUser = existingUserResult.Value;

            if (securityService.VerifyPassword(user.Password, existingUser.HashedPassword))
                return Result<UserResponseDto>.Success(new UserResponseDto()
                {
                    Id = existingUser.id,
                    Name = existingUser.Name,
                    UserName = existingUser.UserName
                });

            return Result<UserResponseDto>.Failure("Username or password incorrect.");
        }

        public Result<UserResponseDto> RegisterUser(RegisterUserDto userToRegister)
        {
            var existingUserResult = GetUserByUserName(userToRegister.UserName);
            if (existingUserResult.IsSuccess)
                return Result<UserResponseDto>.Failure("User Exists");

            var hashedPassword = securityService.HashPassword(userToRegister.Password);

            var user = userRepository.SaveUser(new User()
            {
                UserName = userToRegister.UserName,
                Name = userToRegister.Name,
                HashedPassword = hashedPassword
            }).Result;

            return Result<UserResponseDto>.Success(new UserResponseDto() 
            {
                Id = user.id,
                Name = user.UserName,
                UserName = user.Name
            });

        }

        public Result<User> GetUserByUserName(string username)
        {
            var user = userRepository.GetUserByUserName(username).Result;
            if (user == null)
                return Result<User>.Failure("User not found");

            return Result<User>.Success(new User()
            {
                UserName = user.UserName,
                id = user.id,
                Name = user.Name,
                HashedPassword = user.HashedPassword
            });
        }
    }
}
