using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repository
{
    public class UsuariosRepository
    {
        private readonly string _connectionString;

        public UsuariosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> CadastrarUsuariosDB(Usuarios usuarios)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO Usuarios (Nome, Email, Endereco) VALUES (@Nome, @Email, @Endereco);" +
                          "SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, usuarios);
            }
        }

        public async Task<IEnumerable<Usuarios>> ListarUsuariosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Usuarios";
                return await conn.QueryAsync<Usuarios>(sql);
            }
        }
    }
}
