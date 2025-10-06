using MySql.Data.MySqlClient;
using PruebaTecnicaAPI.Data;
using PruebaTecnicaAPI.Model;
using System.Data;

namespace PruebaTecnicaAPI.Dao
{
    public class UsuarioDao
    {
        private readonly IDatabase _connectionService;

        public UsuarioDao(IDatabase connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<List<UsuarioData>> GetUsuarios()
        {
            List<UsuarioData> usuarios = new();

            using var conn = _connectionService.GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("CALL USUARIOCRUD(?,?,?,?,?)", conn);
            
            cmd.Parameters.Add(new MySqlParameter { Value = "L" });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                usuarios.Add(new UsuarioData
                {
                    Id = reader.GetInt32("USUARIOID"),
                    Nombre = reader.IsDBNull(reader.GetOrdinal("NOMBRE")) ? string.Empty : reader.GetString("NOMBRE"),
                    FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FECHANACIMIENTO")) ? DateTime.MinValue : reader.GetDateTime("FECHANACIMIENTO"),
                    Sexo = reader.IsDBNull(reader.GetOrdinal("SEXO")) ? string.Empty : reader.GetString("SEXO")
                });
            }

            return usuarios;
        }

        public async Task<UsuarioData?> GetUsuarioById(int id)
        {
            using var conn = _connectionService.GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("CALL USUARIOCRUD(?,?,?,?,?)", conn);
            
            cmd.Parameters.Add(new MySqlParameter { Value = "R" });
            cmd.Parameters.Add(new MySqlParameter { Value = id });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new UsuarioData
                {
                    Id = reader.GetInt32("USUARIOID"),
                    Nombre = reader.IsDBNull(reader.GetOrdinal("NOMBRE")) ? string.Empty : reader.GetString("NOMBRE"),
                    FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FECHANACIMIENTO")) ? DateTime.MinValue : reader.GetDateTime("FECHANACIMIENTO"),
                    Sexo = reader.IsDBNull(reader.GetOrdinal("SEXO")) ? string.Empty : reader.GetString("SEXO")
                };
            }

            return null;
        }

        public async Task<int> CreateUsuario(UsuarioData usuario)
        {
            using var conn = _connectionService.GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("CALL USUARIOCRUD(?,?,?,?,?)", conn);
            
            cmd.Parameters.Add(new MySqlParameter { Value = "C" });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = usuario.Nombre });
            cmd.Parameters.Add(new MySqlParameter { Value = usuario.FechaNacimiento });
            cmd.Parameters.Add(new MySqlParameter { Value = usuario.Sexo });

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return reader.GetInt32("USUARIOID");
            }

            return 0;
        }

        public async Task<bool> UpdateUsuario(UsuarioData usuario)
        {
            using var conn = _connectionService.GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("CALL USUARIOCRUD(?,?,?,?,?)", conn);
            
            cmd.Parameters.Add(new MySqlParameter { Value = "U" });
            cmd.Parameters.Add(new MySqlParameter { Value = usuario.Id });
            cmd.Parameters.Add(new MySqlParameter { Value = usuario.Nombre });
            cmd.Parameters.Add(new MySqlParameter { Value = usuario.FechaNacimiento });
            cmd.Parameters.Add(new MySqlParameter { Value = usuario.Sexo });

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var filasAfectadas = reader.GetInt32("filas_afectadas");
                return filasAfectadas > 0;
            }

            return false;
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            using var conn = _connectionService.GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("CALL USUARIOCRUD(?,?,?,?,?)", conn);
            
            cmd.Parameters.Add(new MySqlParameter { Value = "D" });
            cmd.Parameters.Add(new MySqlParameter { Value = id });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });
            cmd.Parameters.Add(new MySqlParameter { Value = DBNull.Value });

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var filasAfectadas = reader.GetInt32("filas_afectadas");
                return filasAfectadas > 0;
            }

            return false;
        }
    }
}
