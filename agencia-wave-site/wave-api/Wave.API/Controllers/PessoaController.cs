using Microsoft.AspNetCore.Mvc;
using Wave.Application.Commands;
using Wave.Application.Interfaces.Service;
using Wave.Application.Queries;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaQuery>>> RecuperarListaPessoas()
        {
            var pessoas = await _pessoaService.RecuperarListaPessoasAsync();

            if (pessoas == null)
                return NoContent();
            else
                return Ok(pessoas);
        }

        [HttpGet("{codigoPessoa}")]
        public async Task<ActionResult<PessoaQuery>> RecuperarPessoa([FromRoute] int codigoPessoa)
        {
            if (codigoPessoa == 0)
                return BadRequest("CodigoPessoa inválido.");

            var pessoa = await _pessoaService.RecuperarPessoaAsync(codigoPessoa);

            if (pessoa == null)
                return NotFound("Pessoa não encontrada.");
            else
                return Ok(pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<PessoaQuery>> AdicionarPessoa([FromBody] PessoaCommand pessoaCommand)
        {
            if (pessoaCommand == null)
                return BadRequest("Pessoa inválida.");

            await _pessoaService.AdicionarPessoaAsync(pessoaCommand);

            return Created();
        }

        [HttpPut("{codigoPessoa}")]
        public async Task<ActionResult> AlterarPessoa([FromBody] PessoaCommand pessoaCommand, [FromRoute] int codigoPessoa)
        {
            if (codigoPessoa == 0)
                return BadRequest("CodigoPessoa inválido.");

            if (pessoaCommand == null)
                return BadRequest("Pessoa inválida.");

            await _pessoaService.AlterarPessoaAsync(pessoaCommand, codigoPessoa);

            return Ok();
        }

        [HttpDelete("{codigoPessoa}")]
        public async Task<ActionResult<bool>> DeletarPessoa([FromRoute] int codigoPessoa)
        {
            if (codigoPessoa == 0)
                return BadRequest("CodigoPessoa inválido.");

            var result = await _pessoaService.DeletarPessoaAsync(codigoPessoa);

            if (result == false)
                return NotFound("Pessoa não encontrada.");
            else
                return Ok($"Resultado: {result}\nPessoa deletada com sucesso!");
        }
    }
}
