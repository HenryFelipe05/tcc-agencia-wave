using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Queries;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public AutenticacaoController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<PessoaQuery>> AdicionarNovoUsuario([FromBody] NovoUsuarioCommand novoUsuarioCommand)
        {
            //adiciona a pessoa
            PessoaCommand pessoaCommand = new PessoaCommand
            {
                Nome = novoUsuarioCommand.Nome,
                Sobrenome = novoUsuarioCommand.Sobrenome,
                Documento = novoUsuarioCommand.Documento,
                DataNascimento = novoUsuarioCommand.DataNascimento,
                CodigoGenero = novoUsuarioCommand.CodigoGenero,
                CodigoTipoPessoa = novoUsuarioCommand.CodigoTipoPessoa
            };

            await _pessoaService.AdicionarPessoaAsync(pessoaCommand);

            //adiciona o usuario

            //autentica o usuario cadastrado

            return Ok();
        } 
    }
}
