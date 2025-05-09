using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaleriaController : ControllerBase
    {
        private IGaleriaService _galeriaService;

        public GaleriaController(IGaleriaService galeriaService)
        {
            _galeriaService = galeriaService;
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ItemGaleria>> CriarItem(ItemGaleria itemGaleria, int codigoUsuario)
        {
            return await _galeriaService.CriarItemGaleria(itemGaleria, codigoUsuario);
        }

        [HttpGet("baixar/{codigoItemGaleria}")]
        public async Task<IActionResult> BaixarItemAsync(int codigoItemGaleria)
        {
            try
            {
                var comando = new ItemGaleriaCommand { CodigoItemGaleria = codigoItemGaleria };
                var arquivo = await _galeriaService.BaixarItemAsync(comando);

                return File(arquivo, "application/octet-stream"); // Retorna o arquivo para download
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Retorna erro caso ocorra
            }
        }

        [HttpGet("itens")]
        public async Task<IActionResult> BuscarItensAsync([FromQuery] ItemGaleriaQuery itemGaleriaQuery)
        {
            try
            {
                var itens = await _galeriaService.BuscarItensAsync(itemGaleriaQuery);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("favoritar")]
        public async Task<IActionResult> FavoritarItemAsync([FromBody] Favorito favorito)
        {
            try
            {
                await _galeriaService.FavoritarItemAsync(favorito.CodigoItemGaleria, favorito.CodigoUsuario);
                return Ok("Item favoritado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Alterar Item")]
        public async Task<IActionResult> AlterarItemAsync([FromBody] ItemGaleria itemGaleria, [FromQuery] int codigoUsuario)
        {
            try
            {
                await _galeriaService.AlterarItemAsync(itemGaleria, codigoUsuario);
                return Ok("Item Alterado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Excluir Item")]
        public async Task<ActionResult> ExcluirItemAsync(int codigoItemGaleria, int codigoUsuario)
        {
            try
            {
                await _galeriaService.ExcluirItemAsync(codigoItemGaleria, codigoUsuario);
                return Ok("Item Excluido com exito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
