using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositories
{
    public class CarrinhoRepository
    {
        private readonly string _connectionString;

        public CarrinhoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> AdicionarProdutoCarrinhoDB(int usuarioId, int produtoId, int quantidade)
        {
            using (var conn = Connection)
            {
                //Validação de estoque ao adicionar ao carrinho
                var sqlEstoque = "SELECT QuantidadeEstoque FROM Produtos WHERE Id = @ProdutoId";
                var quantidadeEstoque = await conn.ExecuteScalarAsync<int>(sqlEstoque, new { ProdutoId = produtoId });

                //Verificar se há quantidade suficiente em estoque
                if (quantidade > quantidadeEstoque)
                {
                    throw new InvalidOperationException("Produto sem estoque!!");
                }

                //Consultaa se o produto já tá no carrinho
                var sqlVerificarProdutoCarrinho = "SELECT COUNT(*) FROM Carrinho WHERE UsuarioId = @UsuarioId AND ProdutoId = @ProdutoId";
                var produtoExists = await conn.ExecuteScalarAsync<int>(sqlVerificarProdutoCarrinho, new { UsuarioId = usuarioId, ProdutoId = produtoId });

                if (produtoExists > 0)
                {
                    //Atualizar quantidade se o produto já tá no carrinho
                    var sqlQuantidade = "UPDATE Carrinho SET Quantidade = Quantidade + @Quantidade WHERE UsuarioId = @UsuarioId AND ProdutoId = @ProdutoId";
                    return await conn.ExecuteAsync(sqlQuantidade, new { UsuarioId = usuarioId, ProdutoId = produtoId, Quantidade = quantidade });
                }
                else
                {
                    //Adicionar novo item
                    var sqlAdicionar = "INSERT INTO Carrinho (UsuarioId, ProdutoId, Quantidade) VALUES (@UsuarioId, @ProdutoId, @Quantidade)";
                    return await conn.ExecuteAsync(sqlAdicionar, new { UsuarioId = usuarioId, ProdutoId = produtoId, Quantidade = quantidade });
                }
            }
        }

        public async Task<int> RemoverProdutoCarrinhoDB(int usuarioId, int produtoId)
        {
            using (var conn = Connection)
            {
                var sql = "DELETE FROM Carrinho WHERE UsuarioId = @UsuarioId AND ProdutoId = @ProdutoId";

                return await conn.ExecuteAsync(sql, new { UsuarioId = usuarioId, ProdutoId = produtoId });
            }
        }

        public async Task<IEnumerable<dynamic>> ConsultarCarrinhoDB(int usuarioId)
        {
            using (var conn = Connection)
            {
                var sql = @"SELECT c.ProdutoId, p.Nome, p.Descricao, c.Quantidade, p.Preco, 
                            (c.Quantidade * p.Preco) AS ValorTotal
                            FROM Carrinho c
                            JOIN Produtos p ON c.ProdutoId = p.Id
                            WHERE c.UsuarioId = @UsuarioId";

                return await conn.QueryAsync<dynamic>(sql, new { UsuarioId = usuarioId });
            }
        }

        public async Task<decimal> CalcularValorTotalCarrinho(int usuarioId)
        {
            using (var conn = Connection)
            {
                var sql = @"SELECT SUM(c.Quantidade * p.Preco) 
                            FROM Carrinho c
                            JOIN Produtos p ON c.ProdutoId = p.Id
                            WHERE c.UsuarioId = @UsuarioId";

                return await conn.ExecuteScalarAsync<decimal>(sql, new { UsuarioId = usuarioId });
            }
        }
    }
}