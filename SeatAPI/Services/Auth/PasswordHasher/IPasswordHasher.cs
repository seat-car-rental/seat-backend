using System.Security.Cryptography;
using System.Text;

namespace SeatAPI.Services.Auth.PasswordHasher
{
    public interface IPasswordHasher
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
