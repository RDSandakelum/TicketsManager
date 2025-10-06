using System.Security.Authentication;
using AutoMapper;
using MediatR;
using TicketsManager.Business.Repository;
using TicketsManager.Business.Services;
using TicketsManager.Common.Database;
using TicketsManager.Common.Dto;
using TicketsManager.Common.Services.Definitions;
using TicketsManager.Common.Types;

namespace TicketsManager.Business.Actions.Users;

public class LoginUserCommand : IRequest<LoginUserCommandResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginUserCommandResponse
{
    public LoginUserResponseDto User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class LoginUserCommandHandler : RepositoryAccess,IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
{
    private readonly IPasswordService passwordService;
    private readonly IMapper mapper;
    private readonly TokenService tokenService;
    public LoginUserCommandHandler(ITicketsManagerDbContext ticketsManagerDbContext, IPasswordService passwordService, IMapper mapper, TokenService tokenService) : base(ticketsManagerDbContext)
    {
        this.passwordService = passwordService;
        this.mapper = mapper;
        this.tokenService = tokenService;
    }
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetUserByUsername(request.Username) ?? throw new InvalidCredentialException("Invalid username or password.");
        var isPasswordValid = passwordService.VerifyPassword(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

        if (!isPasswordValid)
            throw new InvalidCredentialException("Invalid username or password.");

        Tokens tokens = tokenService.GenerateTokens(existingUser);

        existingUser.RefreshToken = tokens.RefreshToken;
        existingUser.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddDays(7);

        await unitOfWork.SaveChangesAsync();

        return new LoginUserCommandResponse()
        {
            User = mapper.Map<LoginUserResponseDto>(existingUser),
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
        };
    }
}
