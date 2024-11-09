using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repository
{
    public class ProdutosRepository
    {
        private readonly string _connectionString;

        public ProdutosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public ProdutosRepository(string connectionString, ProdutosRepository produtosRepository)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> CadastrarProdutoDB(Produtos produto)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO Produtos (Nome, Descricao, Preco, QuantidadeEstoque) VALUES (@Nome, @Descricao, @Preco, @QuantidadeEstoque);" +
                "SELECT LAST_INSERT_ID()";
                return await conn.ExecuteScalarAsync<int>(sql, produto);
            }
        }

        public async Task<IEnumerable<Produtos>> ListarProdutosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos";
                return await conn.QueryAsync<Produtos>(sql);
            }
        }

        public async Task<int> AtualizarProdutoDB(Produtos produtos)
        {
            using (var conn = Connection)
            {
                var sql = "UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco, " +
                          "QuantidadeEstoque = @QuantidadeEstoque WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, produtos);
            }
        }

        public async Task<int> ExcluirProdutoDB(int id)
        {
            using (var conn = Connection)
            {
                //Verificando se o produto está em um carrinho ativo

                var sqlVerificarCarrinho = "SELECT COUNT(*) FROM Carrinho WHERE ProdutoId = @Id";

                var sqlExcluirProduto = "DELETE  FROM Produtos WHERE Id = @Id";
                return await conn.ExecuteAsync(sqlExcluirProduto, new { Id = id });
            }
        }
        public async Task<IEnumerable<Produtos>> BuscarProduto(string? Nome = null, string? Descricao = null, decimal? Preco = null)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos WHERE 1=1";

                if (!string.IsNullOrEmpty(Nome))
                {
                    sql += " AND Nome LIKE @Nome";
                }
                if (!string.IsNullOrEmpty(Descricao))
                {
                    sql += " AND Descricao LIKE @Descricao";
                }
                if (Preco.HasValue)
                {
                    sql += " AND Preco LIKE @Preco";
                }

                return await conn.QueryAsync<Produtos>(sql, new { Nome = $"%{Nome}%", Descricao = $"%{Descricao}%", Preco = $"%{Preco}%" });
            }
        }

    }
}
