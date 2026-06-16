using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ARTNEST.Models;

namespace ARTNEST.DAL
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly DbConnectionFactory _factory;
        private readonly ILogger<ArtworkRepository> _logger;

        public ArtworkRepository(DbConnectionFactory factory, ILogger<ArtworkRepository> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public List<Artwork> GetAllArtworks()
        {
            return SearchArtworks(null, null, null, null);
        }

        public List<Artwork> SearchArtworks(string? searchQuery, string? filterArtist, string? filterMuseum, int? filterYear)
        {
            var artworks = new List<Artwork>();
            try
            {
                using var connection = _factory.Create();
                connection.Open();

                var sql = @"SELECT Id, Title, Artist, Museum, ImageUrl, Description, Year
                            FROM Artworks WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(searchQuery))
                    sql += " AND (Title LIKE @Search OR Artist LIKE @Search OR Museum LIKE @Search)";
                if (!string.IsNullOrWhiteSpace(filterArtist))
                    sql += " AND Artist = @Artist";
                if (!string.IsNullOrWhiteSpace(filterMuseum))
                    sql += " AND Museum = @Museum";
                if (filterYear.HasValue)
                    sql += " AND Year = @Year";

                sql += " ORDER BY Title";

                using var command = new SqlCommand(sql, connection);

                if (!string.IsNullOrWhiteSpace(searchQuery))
                    command.Parameters.AddWithValue("@Search", $"%{searchQuery}%");
                if (!string.IsNullOrWhiteSpace(filterArtist))
                    command.Parameters.AddWithValue("@Artist", filterArtist);
                if (!string.IsNullOrWhiteSpace(filterMuseum))
                    command.Parameters.AddWithValue("@Museum", filterMuseum);
                if (filterYear.HasValue)
                    command.Parameters.AddWithValue("@Year", filterYear.Value);

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
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while searching artworks.");
                return new List<Artwork>();
            }
            return artworks;
        }

        public List<string> GetDistinctArtists()
        {
            var list = new List<string>();
            try
            {
                using var connection = _factory.Create();
                connection.Open();
                using var cmd = new SqlCommand("SELECT DISTINCT Artist FROM Artworks ORDER BY Artist", connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read()) list.Add(reader[0]?.ToString() ?? "");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while loading distinct artists.");
            }
            return list;
        }

        public List<string> GetDistinctMuseums()
        {
            var list = new List<string>();
            try
            {
                using var connection = _factory.Create();
                connection.Open();
                using var cmd = new SqlCommand("SELECT DISTINCT Museum FROM Artworks WHERE Museum != '' ORDER BY Museum", connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read()) list.Add(reader[0]?.ToString() ?? "");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while loading distinct museums.");
            }
            return list;
        }

        public Artwork? GetById(int id)
        {
            try
            {
                using var connection = _factory.Create();
                connection.Open();
                const string query = "SELECT Id, Title, Artist, Museum, ImageUrl, Description, Year FROM Artworks WHERE Id = @Id";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Artwork
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"]?.ToString() ?? string.Empty,
                        Artist = reader["Artist"]?.ToString() ?? string.Empty,
                        Museum = reader["Museum"]?.ToString() ?? string.Empty,
                        ImageUrl = reader["ImageUrl"]?.ToString() ?? string.Empty,
                        Description = reader["Description"]?.ToString() ?? string.Empty,
                        Year = Convert.ToInt32(reader["Year"])
                    };
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while loading artwork by id.");
            }
            return null;
        }

        public void Insert(Artwork artwork)
        {
            try
            {
                using var connection = _factory.Create();
                connection.Open();
                const string query = @"
                    INSERT INTO Artworks (Title, Artist, Museum, ImageUrl, Description, Year)
                    VALUES (@Title, @Artist, @Museum, @ImageUrl, @Description, @Year)";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", artwork.Title);
                command.Parameters.AddWithValue("@Artist", artwork.Artist);
                command.Parameters.AddWithValue("@Museum", artwork.Museum);
                command.Parameters.AddWithValue("@ImageUrl", artwork.ImageUrl);
                command.Parameters.AddWithValue("@Description", artwork.Description);
                command.Parameters.AddWithValue("@Year", artwork.Year);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while inserting artwork.");
                throw;
            }
        }

        public void Update(Artwork artwork)
        {
            try
            {
                using var connection = _factory.Create();
                connection.Open();
                const string query = @"
                    UPDATE Artworks
                    SET Title = @Title, Artist = @Artist, Museum = @Museum,
                        ImageUrl = @ImageUrl, Description = @Description, Year = @Year
                    WHERE Id = @Id";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", artwork.Id);
                command.Parameters.AddWithValue("@Title", artwork.Title);
                command.Parameters.AddWithValue("@Artist", artwork.Artist);
                command.Parameters.AddWithValue("@Museum", artwork.Museum);
                command.Parameters.AddWithValue("@ImageUrl", artwork.ImageUrl);
                command.Parameters.AddWithValue("@Description", artwork.Description);
                command.Parameters.AddWithValue("@Year", artwork.Year);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error while updating artwork.");
                throw;
            }
        }
    }
}