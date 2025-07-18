namespace TicketsManager.Common.Services.Definitions;
public interface IPasswordService
{
    string GenerateSalt(int size);
    string HashPassword(string password, string salt);
    bool VerifyPassword(string inputPassword, string storedHash, string storedSalt);
}
