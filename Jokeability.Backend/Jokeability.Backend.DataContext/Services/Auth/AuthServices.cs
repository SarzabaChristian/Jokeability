using Jokeability.Backend.DataContext.Data;
using Jokeability.Backend.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DataContext.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly JokeabilityDBContext _context;
        public AuthServices(JokeabilityDBContext context)
        {
            _context = context;
        }

        public async Task<bool> isExistUser(string username)
        {
            return await _context.Users.Where(x => x.Username == username).AnyAsync();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return null;

            if (!DecryptPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool DecryptPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        public async Task<User> Register(User newUser, string password)
        {
            var userUsername = newUser.Username.ToString().ToLower();
            if (await isExistUser(userUsername))
                return null;

            byte[] passwordSalt, passwordHash;
            EncryptPassword(password, out passwordSalt, out passwordHash);
            newUser.PasswordSalt = passwordSalt;
            newUser.PasswordHash = passwordHash;
            newUser.Username = userUsername;
            newUser.isWithBadge = false;
            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        private void EncryptPassword(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
