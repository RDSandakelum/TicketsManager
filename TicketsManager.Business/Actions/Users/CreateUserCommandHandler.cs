using MediatR;
using TicketsManager.Business.Repository;
using TicketsManager.Common.Database;
using TicketsManager.Common.Dto;
using TicketsManager.Common.Repository;
using TicketsManager.Common.Services.Definitions;

namespace TicketsManager.Business.Actions.Users;

public class CreateUserCommand : IRequest<CreateUserCommandResponse>
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateUserCommandResponse
{
    public UserDto User { get; set; }
}

public class CreateUserCommandHandler : RepositoryAccess, IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly IPasswordService passwordService;
    public CreateUserCommandHandler(ITicketsManagerDbContext ticketsManagerDbContext, IPasswordService passwordService) : base(ticketsManagerDbContext)
    {
        this.passwordService = passwordService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetUserByUsername(request.Username);

        if (existingUser == null)
        {
            var passwordSalt = passwordService.GenerateSalt(16);
            var passwordHash = passwordService.HashPassword(request.Password, passwordSalt);

            var newUser = new Common.Entity.UserEntity()
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                NormalizedUsername = request.Username.ToLower(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToLower(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
            };

            await userRepository.CreateUser(newUser);

            await unitOfWork.SaveChangesAsync();

            return new CreateUserCommandResponse
            {
                User = await userRepository.GetUserById(newUser.UserId)
            };
        }
        else
        {
            throw new InvalidOperationException("User with this username already exists.");
        }
    }
}
