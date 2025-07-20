using MediatR;
using TicketsManager.Business.Repository;
using TicketsManager.Common.Database;
using TicketsManager.Common.Dto;
using TicketsManager.Common.Services.Definitions;

namespace TicketsManager.Business.Actions.Users
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandResponse
    {
        public UserDto User { get; set; }
    }

    public class LoginUserCommandHandler : RepositoryAccess,IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly IPasswordService passwordService;
        public LoginUserCommandHandler(ITicketsManagerDbContext ticketsManagerDbContext, IPasswordService passwordService) : base(ticketsManagerDbContext)
        {
            this.passwordService = passwordService;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserByUsername(request.Username);

            if (existingUser == null)
            {
                return null;
            }

            var isPasswordValid = passwordService.VerifyPassword(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

            if (isPasswordValid)
            {
                return new LoginUserCommandResponse()
                {
                    User = new UserDto()
                    {
                        UserId = existingUser.UserId,
                        FirstName = existingUser.FirstName,
                        LastName = existingUser.LastName,
                        Username = existingUser.Username,
                        Email = existingUser.Email
                    }
                };
            }

            return null;
        }
    }
}
