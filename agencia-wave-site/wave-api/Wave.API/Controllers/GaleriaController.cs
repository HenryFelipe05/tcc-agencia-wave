using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services;
using Wave.Application.Services.Interfaces;
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
        public async Task<ActionResult<ItemGaleria>> CriarItem([FromBody] CriarItemGaleriaCommand command)
        {
            try
            {
                var itemGaleria = await _galeriaService.CriarItemGaleriaAsync(command);
                return Ok(itemGaleria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpGet("Filtrar")]
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

        [HttpPut("{codigoItemGaleria}")]
        public async Task<IActionResult> AlterarItemAsync([FromBody] ItemGaleria itemGaleria, [FromQuery] int codigoUsuario)
        {
            try
            {
                await _galeriaService.AlterarItemAsync(itemGaleria, codigoUsuario);
                return Ok(new { message = "Item alterado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{codigoItemGaleria}")]
        public async Task<IActionResult> DeletarItemAsync([FromBody] ExcluirItemGaleriaCommand command)
        {
            try
            {
                await _galeriaService.ExcluirItemAsync(command);
                return Ok(new { message = "Item deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
