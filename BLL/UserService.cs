using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ARTNEST.Models;
using ARTNEST.DAL;

namespace ARTNEST.BLL
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool RegisterUser(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (_userRepository.UserExists(email))
            {
                return false;
            }

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = HashPassword(password),
                 CreatedAt = DateTime.Now 
            };

            _userRepository.SaveUser(user);
            return true;
        }

        public User? LoginUser(string email, string password)
        {
            User? user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            bool isValid = VerifyPassword(password, user.PasswordHash);

            if (!isValid)
            {
                return null;
            }

            return user;
        }

        public User? GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public bool UpdateUserSettings(int userId, string name, string email, string? newPassword)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            User? existingUser = _userRepository.GetUserById(userId);

            if (existingUser == null)
            {
                return false;
            }

            if (_userRepository.EmailExistsForAnotherUser(email, userId))
            {
                return false;
            }

            existingUser.Name = name;
            existingUser.Email = email;

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                existingUser.PasswordHash = HashPassword(newPassword);
            }

            _userRepository.UpdateUser(existingUser);
            return true;
        }



        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        private bool VerifyPassword(string password, string storedPasswordHash)
        {
            string[] parts = storedPasswordHash.Split('.');

            byte[] salt = Convert.FromBase64String(parts[0]);
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            return hashedPassword == parts[1];
        }
    }
}