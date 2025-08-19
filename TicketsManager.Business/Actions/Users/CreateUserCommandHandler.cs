using System.Text.Json;
using AutoMapper;
using FluentValidation;
using MediatR;
using TicketsManager.Business.Exceptions;
using TicketsManager.Business.Repository;
using TicketsManager.Common.Database;
using TicketsManager.Common.Dto;
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
    private readonly IMapper mapper;
    private readonly IValidator<CreateUserCommand> commandValidator;

    public CreateUserCommandHandler(ITicketsManagerDbContext ticketsManagerDbContext, IPasswordService passwordService, IMapper mapper, IValidator<CreateUserCommand> commandValidator) : base(ticketsManagerDbContext)
    {
        this.passwordService = passwordService;
        this.mapper = mapper;
        this.commandValidator = commandValidator;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = commandValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new CommandValidationExeption(JsonSerializer.Serialize(validationResult.ToDictionary()));
        }

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

            var userEntity = await userRepository.GetUserById(newUser.UserId);

            return new CreateUserCommandResponse
            {
                User = mapper.Map<UserDto>(userEntity)
            };
        }
        else
        {
            throw new InvalidOperationException("User with this username already exists.");
        }
    }
}
