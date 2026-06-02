using System;
using System.Collections.Generic;
using System.Linq;
using ARTNEST.DAL;
using ARTNEST.Models;

namespace ArtNest.Tests.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1;

        public void SaveUser(User user)
        {
       
            if (user.Id == 0)
                user.Id = _nextId++;

            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public bool UserExists(string email)
        {
            return _users.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool EmailExistsForAnotherUser(string email, int userId)
        {
            
            return _users.Any(u =>
                u.Id != userId &&
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateUser(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing == null) return;

            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.PasswordHash = user.PasswordHash;
        }
    }
}