using LojaApi.Models;

namespace LojaApi.Repository
{
    public interface IProdutosRepository
    {
        Task<int> AtualizarProdutoDB(Produtos produtos);
        Task<int> CadastrarProdutoDB(Produtos produtos);
        Task<int> ExcluirProdutoDB(int id);
        Task<IEnumerable<Produtos>> ListarProdutosDB();
    }
}