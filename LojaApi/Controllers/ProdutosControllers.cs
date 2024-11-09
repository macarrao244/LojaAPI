using LojaApi.Models;
using LojaApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosRepository _produtosRepository;

        public ProdutosController(ProdutosRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        [HttpGet("listar-Produtos")]
        public async Task<IActionResult> ListarProdutosDB()
        {
            var produtos = await _produtosRepository.ListarProdutosDB();

            return Ok(produtos);
        }


        [HttpPost("cadastrar-produtos")]
        public async Task<IActionResult> CadastrarProdutoDB([FromBody] Produtos produtos)
        {
            var produtosId = await _produtosRepository.CadastrarProdutoDB(produtos);

            return Ok(new { mensagem = "Produto registrado com sucesso", produtosId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProdutoDB(int id, [FromBody] Produtos produtos)
        {
            produtos.Id = id;
            await _produtosRepository.AtualizarProdutoDB(produtos);

            return Ok(new { mensagem = "Produto atualizado" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProdutoDB(int id)
        {


            try
            {
                await _produtosRepository.ExcluirProdutoDB(id);

                return Ok(new { mensagem = "Produto excluído com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("buscar-produto")]
        public async Task<IActionResult> BuscarProduto([FromQuery] string? Nome, [FromQuery] string? Descricao, [FromQuery] decimal? Preco)
        {
            var produto = await _produtosRepository.BuscarProduto(Nome, Descricao, Preco);
            return Ok(produto);
        }


    }
}
