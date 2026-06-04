using Microsoft.Data.SqlClient;
using ARTNEST.Models;

namespace ARTNEST.DAL
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly DbConnectionFactory _factory;

        public WishlistRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public bool IsAlreadyInWishlist(int userId, int artworkId)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                SELECT COUNT(1)
                FROM WishlistItems
                WHERE UserId = @UserId AND ArtworkId = @ArtworkId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ArtworkId", artworkId);

            int count = (int)command.ExecuteScalar()!;
            return count > 0;
        }

        public void SaveToWishlist(int userId, int artworkId)
        {
            using var connection = _factory.Create();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                const string checkQuery = @"
                    SELECT COUNT(1)
                    FROM WishlistItems
                    WHERE UserId = @UserId AND ArtworkId = @ArtworkId";

                using var checkCommand = new SqlCommand(checkQuery, connection, transaction);
                checkCommand.Parameters.AddWithValue("@UserId", userId);
                checkCommand.Parameters.AddWithValue("@ArtworkId", artworkId);

                int count = (int)checkCommand.ExecuteScalar()!;

                if (count == 0)
                {
                    const string insertQuery = @"
                        INSERT INTO WishlistItems (UserId, ArtworkId, SavedDate)
                        VALUES (@UserId, @ArtworkId, @SavedDate)";

                    using var insertCommand = new SqlCommand(insertQuery, connection, transaction);
                    insertCommand.Parameters.AddWithValue("@UserId", userId);
                    insertCommand.Parameters.AddWithValue("@ArtworkId", artworkId);
                    insertCommand.Parameters.AddWithValue("@SavedDate", DateTime.Now);

                    insertCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void RemoveFromWishlist(int userId, int artworkId)
        {
            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                DELETE FROM WishlistItems
                WHERE UserId = @UserId AND ArtworkId = @ArtworkId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@ArtworkId", artworkId);

            command.ExecuteNonQuery();
        }

        public List<Artwork> GetWishlistByUserId(int userId)
        {
            var artworks = new List<Artwork>();

            using var connection = _factory.Create();
            connection.Open();

            const string query = @"
                SELECT A.Id, A.Title, A.Artist, A.Museum, A.ImageUrl, A.Description, A.Year
                FROM WishlistItems W
                INNER JOIN Artworks A ON W.ArtworkId = A.Id
                WHERE W.UserId = @UserId
                ORDER BY W.SavedDate DESC";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = command.ExecuteReader();

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