using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Account;
using Wave.Domain.Commands;
using Wave.Domain.Queries;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuararioService;

        public AutenticacaoController(IUsuarioService usuararioService)
        {
            _usuararioService = usuararioService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioQuery>> AdicionarUsuario([FromBody] UsuarioCommand usuarioCommand)
        {
            //if (usuarioCommand == null)
            //    return BadRequest("Dados inválidos.");

            //var emailExiste = await _authenticate.UsuarioExiste(usuarioCommand.Email);

            //if (emailExiste)
            //    return BadRequest("Esse e-mail já possui uma conta vinculada.");

            var usuario = await _usuararioService.AdicionarUsuarioAsync(usuarioCommand);

            if (usuario == null)
                return BadRequest("Ocorreu um erro ao cadastrar o usuário.");
            else
                return Ok(usuario);

            //var token = _authenticate.GerarToken(usuario.CodigoUsuario, usuario.Email);

            //return new TokenUsuarioQuery
            //{
            //    Token = token
            //};
        }
    }
}
