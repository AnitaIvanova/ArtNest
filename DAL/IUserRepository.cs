using ARTNEST.Models;

namespace ARTNEST.DAL
{

    public interface IUserRepository
    {
        void SaveUser(User user);
        User? GetUserByEmail(string email);
        User? GetUserById(int id);
        bool UserExists(string email);
        bool EmailExistsForAnotherUser(string email, int userId);
        void UpdateUser(User user);
    }
}
