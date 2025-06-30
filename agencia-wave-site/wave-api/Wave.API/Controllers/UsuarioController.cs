using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;

namespace Wave.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Recuperar-Dados")]
        [Authorize]
        public async Task<IActionResult> RecuperarDados()
        {
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}");
            Console.WriteLine("=== Claims recebidas ===");
            foreach (var c in claims)
                Console.WriteLine(c);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("Token inválido: claim de ID não encontrada.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("ID do usuário no token é inválido.");

            var usuario = await _usuarioService.RecuperarUsuarioAsync(userId);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuario);
        }
    }
}
