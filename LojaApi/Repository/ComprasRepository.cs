using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repository
{
    public class ComprasRepository
    {
        private readonly string _connectionString;

        public ComprasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly ComprasRepository _comprasRepository;


        public ComprasRepository(string connectionString, ComprasRepository comprasRepository)
        {
            _connectionString = connectionString;
            _comprasRepository = comprasRepository;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> AdicionarProdutoCarrinhoDB(Compras compras)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO Carrinho (UsuarioId, ProdutoId, Quantidade) " +
                          "VALUES (@UsuarioId, @ProdutoId, @Quantidade);" +
                          "SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, compras);
            }
        }
            public async Task<int> RemoverProdutoCarrinhoDB(int id)
            {
 
                using (var conn = Connection)
                {
                    var sqlRemoverProdutoCarrinho = "DELETE FROM Carrinho WHERE Id = @Id";
                    return await conn.ExecuteAsync(sqlRemoverProdutoCarrinho, new { Id = id });
                }
            }

        public async Task<IEnumerable<Compras>> ConsultarCarrinhoDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Carrinho";
                return await conn.QueryAsync<Compras>(sql);
            }
        }

    }
}


