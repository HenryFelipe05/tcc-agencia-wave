﻿using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssinaturaController : ControllerBase
    {
        private readonly IAssinaturaService _assinaturaService;

        public AssinaturaController(IAssinaturaService assinaturaService)
        {
            _assinaturaService = assinaturaService;
        }

        [HttpGet("Usuario/{codigoUsuario}")]
        public async Task<ActionResult<IEnumerable<Assinatura>>> ObterPorUsuario(int codigoUsuario)
        {
            var assinaturas = await _assinaturaService.ObterPorUsuarioIdAsync(codigoUsuario);
            if (!assinaturas.Any())
                return NoContent();

            return Ok(assinaturas);
        }

        [HttpGet("Usuario/{codigoUsuario}/historico")]
        public async Task<ActionResult<IEnumerable<Assinatura>>> ObterGHistorico(int codigoUsuario)
        {
            var historico = await _assinaturaService.ObterHistoricoDeAssinaturasAsync(codigoUsuario);
            if(!historico.Any())
                return NoContent();

            return Ok(historico);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assinatura>>> ObterAssinaturas()
        {
            var assinaturas = await _assinaturaService.ObterTodasAsync();
            if (assinaturas is null)
                return NoContent();
            else
                return Ok(assinaturas);
        }

        [HttpGet("{codigoAssinatura}")]
        public async Task<ActionResult> ObterAssinaturaPorId(Guid codigoAssinatura)
        {
            var assinatura = await _assinaturaService.ObterPorIdAsync(codigoAssinatura);
            if (assinatura is null)
                return NotFound();

            return Ok(assinatura);
        }

        [HttpPost]
        public async Task<ActionResult<Assinatura>> CriarAssinatura(AssinaturaCommand assinaturaCommand)
        {
            var novaAssinatura = await _assinaturaService.CriarAssinaturaAsync(assinaturaCommand);
            return CreatedAtAction(nameof(ObterAssinaturaPorId), new { codigoAssinatura = novaAssinatura.CodigoAssinatura }, novaAssinatura);
        }

        [HttpPut("{codigoAssinatura}")]
        public async Task<IActionResult> AtualizarAssinatura(Guid codigoAssinatura, AssinaturaCommand assinaturaCommand)
        {
            if (codigoAssinatura != assinaturaCommand.CodigoAssinatura)
                return BadRequest();

            var resultado = await _assinaturaService.AtualizarAssinaturaAsync(assinaturaCommand);
            if (resultado is null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpPut("{codigoAssinatura}/cancelar-assinatura")]
        public async Task<IActionResult> CancelamentoAssinatura(Guid codigoAssinatura)
        {
            var mensagem = await _assinaturaService.CancelarAssinaturaAsync(codigoAssinatura);
            return Ok(new { mensagem });
        }


        [HttpDelete("{codigoAssinatura}")]
        public async Task<IActionResult> RemoverAssinatura(Guid codigoAssinatura)
        {
            var resultado = await _assinaturaService.RemoverAssinaturaAsync(codigoAssinatura);
            if (resultado is null)
                return NotFound();

            return Ok(resultado);
        }
    }
}
