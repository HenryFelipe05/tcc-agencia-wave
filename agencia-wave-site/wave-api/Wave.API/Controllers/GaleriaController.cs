using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
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
        private readonly IGaleriaService _galeriaService;

        public GaleriaController(IGaleriaService galeriaService)
        {
            _galeriaService = galeriaService;
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<ItemGaleria>> CriarItem([FromForm] CriarItemGaleriaCommand command)
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
                var item = await _galeriaService.ObterItemAsync(codigoItemGaleria);

                var caminhoArquivo = await _galeriaService.BaixarItemAsync(new ItemGaleriaCommand { CodigoItemGaleria = codigoItemGaleria });

                var bytes = await System.IO.File.ReadAllBytesAsync(caminhoArquivo);

                return File(bytes, "application/octet-stream", item.Arquivo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPut("Alterar Item")]

        public async Task<IActionResult> AlterarItemAsync([FromForm] AlteraItemGaleriaCommand command)
        {
            try
            {
                await _galeriaService.AlterarItemAsync(command);
                return Ok(new { message = "Item alterado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("Excluir Item")]
        public async Task<IActionResult> DeletarItemAsync(int codigoItemGaleria, int codigoUsuario)
        {
            try
            {
                await _galeriaService.ExcluirItemAsync(codigoItemGaleria, codigoUsuario);
                return Ok(new { message = "Item deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pegar todos os itens")]
        public async Task<IActionResult> ObterTodosAsync()
        {
                var itens = await _galeriaService.ObterTodosAsync();
                if (itens == null)
                    throw new Exception("Nenhum Item encontrado");
                else
                return Ok(itens);

        }
    }
}
