using Microsoft.Data.SqlClient;
using ARTNEST.Models;

namespace ARTNEST.Repositories
{
    public class WishlistRepository
    {
        private readonly string _connectionString;

        public WishlistRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public bool IsAlreadyInWishlist(int userId, int artworkId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT COUNT(1)
                             FROM WishlistItems
                             WHERE UserId = @UserId AND ArtworkId = @ArtworkId";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ArtworkId", artworkId);

            int count = (int)command.ExecuteScalar();

            return count > 0;
        }

        public void SaveToWishlist(int userId, int artworkId)
        {
            if (IsAlreadyInWishlist(userId, artworkId))
            {
                return;
            }

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO WishlistItems (UserId, ArtworkId, SavedDate)
                             VALUES (@UserId, @ArtworkId, @SavedDate)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ArtworkId", artworkId);
            command.Parameters.AddWithValue("@SavedDate", DateTime.Now);

            command.ExecuteNonQuery();
        }

        public void RemoveFromWishlist(int userId, int artworkId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE FROM WishlistItems
                             WHERE UserId = @UserId AND ArtworkId = @ArtworkId";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ArtworkId", artworkId);

            command.ExecuteNonQuery();
        }

        public List<Artwork> GetWishlistByUserId(int userId)
        {
            List<Artwork> artworks = new List<Artwork>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT A.Id, A.Title, A.Artist, A.Museum, A.ImageUrl, A.Description, A.Year
                FROM WishlistItems W
                INNER JOIN Artworks A ON W.ArtworkId = A.Id
                WHERE W.UserId = @UserId
                ORDER BY W.SavedDate DESC";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using SqlDataReader reader = command.ExecuteReader();

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

            return artworks;
        }
    }
}