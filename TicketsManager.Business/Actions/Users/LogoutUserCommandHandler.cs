using MediatR;
using TicketsManager.Business.Repository;
using TicketsManager.Common.Database;

namespace TicketsManager.Business.Actions.Users;
public class LogoutUserCommand : IRequest<LogoutUserCommandResponse>
{
    public Guid UserId { get; set; }
}

public class LogoutUserCommandResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

}

public class LogoutUserCommandHandler : RepositoryAccess, IRequestHandler<LogoutUserCommand, LogoutUserCommandResponse>
{
    public LogoutUserCommandHandler(ITicketsManagerDbContext repository) : base(repository)
    {
    }
    public async Task<LogoutUserCommandResponse> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.UserId);

        if (user == null)
            return new LogoutUserCommandResponse
            {
                IsSuccess = false,
                Message = "User not found."
            };

        user.RefreshToken = null;
        user.RefreshTokenExpiry = DateTimeOffset.MinValue;

        await unitOfWork.SaveChangesAsync();

        return new LogoutUserCommandResponse
        {
            IsSuccess = true,
            Message = "User logged out successfully."
        };

    }
}

