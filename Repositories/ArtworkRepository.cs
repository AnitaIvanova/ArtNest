using Microsoft.Data.SqlClient;
using ARTNEST.Models;

namespace ARTNEST.Repositories
{
    public class ArtworkRepository
    {
        private readonly string _connectionString;

        public ArtworkRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<Artwork> GetAllArtworks()
        {
            List<Artwork> artworks = new List<Artwork>();

            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                string query = "SELECT Id, Title, Artist, Museum, ImageUrl, Description, Year FROM Artworks";

                using SqlCommand command = new SqlCommand(query, connection);
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
            }
            catch
            {
                return new List<Artwork>();
            }

            return artworks;
        }
    }
}