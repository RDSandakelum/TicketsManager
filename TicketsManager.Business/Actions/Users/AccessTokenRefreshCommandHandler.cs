using MediatR;
using TicketsManager.Business.Repository;
using TicketsManager.Business.Services;
using TicketsManager.Common.Database;
using TicketsManager.Common.Types;

namespace TicketsManager.Business.Actions.Users
{
    public class AccessTokenRefreshCommand : IRequest<AccessTokenRefreshCommandResponse>
    {
        public Guid userId { get; set; } 
        public string RefreshToken { get; set; }
    }
    public class AccessTokenRefreshCommandResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
    public class AccessTokenRefreshCommandHandler : RepositoryAccess,IRequestHandler<AccessTokenRefreshCommand, AccessTokenRefreshCommandResponse>
    {
        private readonly TokenService tokenService;
        public AccessTokenRefreshCommandHandler(TokenService tokenService, ITicketsManagerDbContext ticketsManagerDbContext) : base(ticketsManagerDbContext)
        {
            this.tokenService = tokenService;
        }

        public async Task<AccessTokenRefreshCommandResponse> Handle(AccessTokenRefreshCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetUserById(request.userId);

            if (existingUser == null)
            {
                return null;
            }

            if (existingUser.RefreshToken != request.RefreshToken || existingUser.RefreshTokenExpiry < DateTimeOffset.UtcNow)
            {
                return null;
            }

            Tokens tokens = tokenService.GenerateTokens(existingUser);

            existingUser.RefreshToken = tokens.RefreshToken;
            existingUser.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddDays(7);

            await unitOfWork.SaveChangesAsync();

            return new AccessTokenRefreshCommandResponse()
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }
    }
}
