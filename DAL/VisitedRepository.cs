using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ARTNEST.Models;

namespace ARTNEST.DAL
{
    public class VisitedRepository : IVisitedRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<VisitedRepository> _logger;

        public VisitedRepository(IConfiguration configuration, ILogger<VisitedRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
            _logger = logger;
        }

        public bool IsVisited(int userId, int artworkId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                const string query = "SELECT COUNT(1) FROM VisitedArtworks WHERE UserId = @UserId AND ArtworkId = @ArtworkId";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@ArtworkId", artworkId);
                return (int)command.ExecuteScalar()! > 0;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while checking visited status.");
                return false;
            }
        }

        public void MarkVisited(int userId, int artworkId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            const string check = "SELECT COUNT(1) FROM VisitedArtworks WHERE UserId = @UserId AND ArtworkId = @ArtworkId";
            using var checkCmd = new SqlCommand(check, connection);
            checkCmd.Parameters.AddWithValue("@UserId", userId);
            checkCmd.Parameters.AddWithValue("@ArtworkId", artworkId);
            if ((int)checkCmd.ExecuteScalar()! > 0) return;

            const string insert = "INSERT INTO VisitedArtworks (UserId, ArtworkId, VisitedDate) VALUES (@UserId, @ArtworkId, @Date)";
            using var cmd = new SqlCommand(insert, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@ArtworkId", artworkId);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();
        }

        public void UnmarkVisited(int userId, int artworkId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            const string query = "DELETE FROM VisitedArtworks WHERE UserId = @UserId AND ArtworkId = @ArtworkId";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@ArtworkId", artworkId);
            cmd.ExecuteNonQuery();
        }

        public List<Artwork> GetVisitedByUserId(int userId)
        {
            var artworks = new List<Artwork>();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                const string query = @"
                    SELECT A.Id, A.Title, A.Artist, A.Museum, A.ImageUrl, A.Description, A.Year
                    FROM VisitedArtworks V
                    INNER JOIN Artworks A ON V.ArtworkId = A.Id
                    WHERE V.UserId = @UserId
                    ORDER BY V.VisitedDate DESC";
                using var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    artworks.Add(new Artwork
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"]?.ToString() ?? string.Empty,
                        Artist = reader["Artist"]?.ToString() ?? string.Empty,
                        Museum = reader["Museum"]?.ToString() ?? string.Empty,
                        ImageUrl = reader["ImageUrl"]?.ToString() ?? string.Empty,
                        Description = reader["Description"]?.ToString() ?? string.Empty,
                        Year = Convert.ToInt32(reader["Year"])
                    });
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while loading visited artworks.");
                return new List<Artwork>();
            }
            return artworks;
        }

        public int CountByUserId(int userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                const string query = "SELECT COUNT(1) FROM VisitedArtworks WHERE UserId = @UserId";
                using var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);
                return (int)cmd.ExecuteScalar()!;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while counting visited artworks.");
                return 0;
            }
        }
    }
}