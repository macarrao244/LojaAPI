using LojaApi.Models;
using LojaApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasControllers : ControllerBase
    {
        private readonly ComprasRepository _comprasRepository;

        public ComprasControllers(ComprasRepository comprasRepository)
        {
            _comprasRepository = comprasRepository;
        }

        [HttpPost("adicionar-carrinho")]
        public async Task<IActionResult> AdicionarProdutoCarrinhoDB([FromBody] Compras compras)
        {
            if (compras == null)
            {
                return BadRequest(new { mensagem = "Compra não pode ser nula." });
            }

            var compraId = await _comprasRepository.AdicionarProdutoCarrinhoDB(compras);
            return Ok(new { mensagem = "Produto adicionado ao carrinho com sucesso", compraId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverProdutoCarrinhoDB(int id)
        {
            try
            {
                await _comprasRepository.RemoverProdutoCarrinhoDB(id);
                return Ok(new { mensagem = "Produto removido do carrinho com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("consultar-carrinho")]
        public async Task<IActionResult> ConsultarCarrinhoDB()
        {
            var compras = await _comprasRepository.ConsultarCarrinhoDB();
            return Ok(compras);
        }
    }
}
