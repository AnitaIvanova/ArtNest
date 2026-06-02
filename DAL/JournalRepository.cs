using Microsoft.Data.SqlClient;
using ARTNEST.Models;

namespace ARTNEST.DAL
{
    public class JournalRepository : IJournalRepository
    {
        private readonly string _connectionString;

        public JournalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<JournalEntry> GetByUserId(int userId)
        {
            var entries = new List<JournalEntry>();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                const string query = @"
                    SELECT J.Id, J.UserId, J.ArtworkId, J.Reflection, J.Date,
                           A.Title, A.Artist, A.Museum, A.ImageUrl, A.Year
                    FROM JournalEntries J
                    INNER JOIN Artworks A ON J.ArtworkId = A.Id
                    WHERE J.UserId = @UserId
                    ORDER BY J.Date DESC";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    entries.Add(new JournalEntry
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        ArtworkId = Convert.ToInt32(reader["ArtworkId"]),
                        Reflection = reader["Reflection"]?.ToString() ?? string.Empty,
                        Date = Convert.ToDateTime(reader["Date"]),
                        Artwork = new Artwork
                        {
                            Id = Convert.ToInt32(reader["ArtworkId"]),
                            Title = reader["Title"]?.ToString() ?? string.Empty,
                            Artist = reader["Artist"]?.ToString() ?? string.Empty,
                            Museum = reader["Museum"]?.ToString() ?? string.Empty,
                            ImageUrl = reader["ImageUrl"]?.ToString() ?? string.Empty,
                            Year = Convert.ToInt32(reader["Year"])
                        }
                    });
                }
            }
            catch { return new List<JournalEntry>(); }
            return entries;
        }

        public int CountByUserId(int userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                const string query = "SELECT COUNT(1) FROM JournalEntries WHERE UserId = @UserId";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                return (int)command.ExecuteScalar()!;
            }
            catch { return 0; }
        }

        public void Add(int userId, int artworkId, string reflection)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            const string query = @"INSERT INTO JournalEntries (UserId, ArtworkId, Reflection, Date)
                                   VALUES (@UserId, @ArtworkId, @Reflection, @Date)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ArtworkId", artworkId);
            command.Parameters.AddWithValue("@Reflection", reflection);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.ExecuteNonQuery();
        }

        public void Delete(int entryId, int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            const string query = "DELETE FROM JournalEntries WHERE Id = @Id AND UserId = @UserId";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", entryId);
            command.Parameters.AddWithValue("@UserId", userId);
            command.ExecuteNonQuery();
        }

        public void Update(int entryId, int userId, string reflection)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            const string query = @"UPDATE JournalEntries SET Reflection = @Reflection
                                   WHERE Id = @Id AND UserId = @UserId";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Reflection", reflection);
            command.Parameters.AddWithValue("@Id", entryId);
            command.Parameters.AddWithValue("@UserId", userId);
            command.ExecuteNonQuery();
        }
    }
}
