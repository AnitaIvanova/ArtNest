using ARTNEST.Models;

namespace ARTNEST.DAL.Mocks
{
   
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public void SaveUser(User user)
        {
            user.Id = _nextId++;
            user.CreatedAt = DateTime.Now;
            _users.Add(user);
        }

        public User? GetUserByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public User? GetUserById(int id) =>
            _users.FirstOrDefault(u => u.Id == id);

        public bool UserExists(string email) =>
            _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public bool EmailExistsForAnotherUser(string email, int userId) =>
            _users.Any(u => u.Id != userId && u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public void UpdateUser(User user)
        {
            User? existing = GetUserById(user.Id);
            if (existing == null) return;

            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.PasswordHash = user.PasswordHash;
        }
    }
}
