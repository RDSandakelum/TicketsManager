namespace TicketsManager.API.Request
{
    public class TokenRefreshRequest
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } 
    }
}
