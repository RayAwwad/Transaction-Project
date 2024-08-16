using System.Security.Cryptography;
using System.Text;

namespace TransactionProject.Models
{
    public class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
               
                var saltedPassword = string.Concat(password, salt);
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);

               
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var hash = HashPassword(enteredPassword, storedSalt);
            return hash == storedHash;
        }
    }
}
