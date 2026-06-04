using Microsoft.Data.SqlClient;
using ARTNEST.Models;

namespace ARTNEST.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _factory;

        public UserRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public void SaveUser(User user)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                INSERT INTO Users (Name, Email, PasswordHash, CreatedAt)
                VALUES (@Name, @Email, @PasswordHash, @CreatedAt)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);

            command.ExecuteNonQuery();
        }

        public User? GetUserByEmail(string email)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                SELECT Id, Name, Email, PasswordHash, CreatedAt
                FROM Users
                WHERE Email = @Email";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"]?.ToString() ?? string.Empty,
                    Email = reader["Email"]?.ToString() ?? string.Empty,
                    PasswordHash = reader["PasswordHash"]?.ToString() ?? string.Empty,
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
            }

            return null;
        }

        public User? GetUserById(int id)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                SELECT Id, Name, Email, PasswordHash, CreatedAt
                FROM Users
                WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"]?.ToString() ?? string.Empty,
                    Email = reader["Email"]?.ToString() ?? string.Empty,
                    PasswordHash = reader["PasswordHash"]?.ToString() ?? string.Empty,
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
            }

            return null;
        }

        public bool UserExists(string email)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            int count = (int)command.ExecuteScalar()!;
            return count > 0;
        }

        public bool EmailExistsForAnotherUser(string email, int userId)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                SELECT COUNT(1)
                FROM Users
                WHERE Email = @Email AND Id != @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Id", userId);

            int count = (int)command.ExecuteScalar()!;
            return count > 0;
        }

        public void UpdateUser(User user)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                UPDATE Users
                SET Name = @Name,
                    Email = @Email,
                    PasswordHash = @PasswordHash
                WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

            command.ExecuteNonQuery();
        }
    }
}