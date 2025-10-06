using MySql.Data.MySqlClient;

namespace PruebaTecnicaAPI.Data
{
    public interface IDatabase
    {
        MySqlConnection GetConnection();
    }

    public class Database : IDatabase
    {
        private readonly string _connectionString;        

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
