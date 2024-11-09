using LojaApi.Models;
using LojaApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosControllers : ControllerBase
    {
        private readonly UsuariosRepository _usuariosRepository;

        public UsuariosControllers(UsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }


        [HttpPost("Cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuariosDB([FromBody] Usuarios usuarios)
        {
            var usuarioId = await _usuariosRepository.CadastrarUsuariosDB(usuarios);

            return Ok(new { mensagem = "Usuário cadastrado com sucesso", usuarioId });
        }

        [HttpGet("listar-usuarios")]
        public async Task<IActionResult> ListarUsuariosDB()
        {
            var usuarios = await _usuariosRepository.ListarUsuariosDB();
            return Ok(usuarios);
        }
    }

}