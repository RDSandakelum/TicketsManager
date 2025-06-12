using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TicketsManager.Business.SecurityService
{
    public class SecurityService : ISecurityService
    {
        public string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = sha256.ComputeHash(inputBytes);
            return Encoding.UTF8.GetString(hashedBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var inputPasswordHash = HashPassword(password);
            return inputPasswordHash == hashedPassword;
        }
    }
}
