
using LojaApi.Models;
using LojaApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class PedidosControllers : ControllerBase
    {       
            private readonly PedidosRepository _pedidosRepository;

            public PedidosControllers(PedidosRepository pedidosRepository)
            {
            _pedidosRepository = pedidosRepository;
            }

        [HttpPost("criar-pedido")]
        public async Task<IActionResult> CriarPedidoDB([FromBody] Pedidos pedidos)
        {
            var pedidoId = await _pedidosRepository.CriarPedidoDB(pedidos);

            return Ok(new { mensagem = "Pedido Feito com sucesso", pedidoId });
        }

        [HttpGet("listar-Pedidos")]
        public async Task<IActionResult> ListarPedidosDB()
        {
            var pedidos = await _pedidosRepository.ListarPedidosDB();
            return Ok(pedidos);
        }

        [HttpGet("consultar-pedido")]
        public async Task<IActionResult> ConsultarPedidoDB()
        {
            var status = await _pedidosRepository.ConsultarPedidoDB();
            return Ok(status);
        }

        [HttpGet("consultar-historico")]
        public async Task<IActionResult> ListarHistoricoPedidosDB()
        {
            var historico = await _pedidosRepository.ListarHistoricoPedidosDB();
            return Ok(historico);
        }
    }

 }

