using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;

namespace Wave.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _favoritoService;

        public FavoritoController(IFavoritoService favoritoService)
        {
            _favoritoService = favoritoService;
        }

        [HttpPost("adicionar")]
        [Authorize]
        public async Task<IActionResult> AdicionarFavorito(int codigoUsuario, int codigoItemGaleria)
        {
            try
            {
                var favorito = await _favoritoService.AdicionarFavoritoAsync(codigoUsuario, codigoItemGaleria);
                return Ok(new { message = "Item adicionado aos favoritos com sucesso.", favorito });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarFavoritos(int codigoUsuario)
        {
            try
            {
                var favoritos = await _favoritoService.ListarFavoritosAsync(codigoUsuario);
                return Ok(favoritos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("remover")]
        public async Task<IActionResult> RemoverFavorito(int codigoUsuario, int codigoItemGaleria)
        {
            try
            {
                await _favoritoService.RemoverFavoritoAsync(codigoUsuario, codigoItemGaleria);
                return Ok(new { message = "Item removido dos favoritos com sucesso." });
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
