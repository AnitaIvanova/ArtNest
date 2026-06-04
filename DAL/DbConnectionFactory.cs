using Microsoft.Data.SqlClient;

namespace ARTNEST.DAL
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}