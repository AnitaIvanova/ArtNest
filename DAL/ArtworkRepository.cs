using Microsoft.Data.SqlClient;
using ARTNEST.Models;

namespace ARTNEST.DAL
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly string _connectionString;

        public ArtworkRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
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
                using var connection = new SqlConnection(_connectionString);
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
            catch { return new List<Artwork>(); }
            return artworks;
        }

        public List<string> GetDistinctArtists()
        {
            var list = new List<string>();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var cmd = new SqlCommand("SELECT DISTINCT Artist FROM Artworks ORDER BY Artist", connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read()) list.Add(reader[0]?.ToString() ?? "");
            }
            catch { }
            return list;
        }

        public List<string> GetDistinctMuseums()
        {
            var list = new List<string>();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var cmd = new SqlCommand("SELECT DISTINCT Museum FROM Artworks WHERE Museum != '' ORDER BY Museum", connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read()) list.Add(reader[0]?.ToString() ?? "");
            }
            catch { }
            return list;
        }

        public Artwork? GetById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
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
            catch { }
            return null;
        }
    }
}
