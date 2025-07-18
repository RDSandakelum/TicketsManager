using System.Security.Cryptography;
using System.Text;
using TicketsManager.Common.Services.Definitions;

namespace TicketsManager.Common.Services;
public class PasswordService : IPasswordService
{
    public string GenerateSalt(int size)
    {
        var salt = new byte[size];
        RandomNumberGenerator.Fill(salt);
        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            var combined = passwordBytes.Concat(saltBytes).ToArray();
            var hash = sha256.ComputeHash(combined);

            return Convert.ToBase64String(hash);
        }
    }

    public bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
    {
        var hashedInput = HashPassword(inputPassword, storedSalt);

        return storedHash.Equals(hashedInput, StringComparison.Ordinal);
    }
}
