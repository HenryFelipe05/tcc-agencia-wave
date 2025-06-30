using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Queries;

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

            var result = await _pessoaService.AdicionarPessoaAsync(pessoaCommand);

            if (result == null)
                return BadRequest();
            else
                return Ok($"Resultado: {result}\nPessoa criada com sucesso!");
        }

        [HttpPut("Alterar/{codigoPessoa}")] 
        public async Task<ActionResult<bool>> AlterarPessoa([FromRoute] int codigoPessoa, [FromBody] PessoaCommand pessoaCommand)
        {
            if (codigoPessoa == 0)
                return BadRequest("CodigoPessoa inválido.");

            if (pessoaCommand == null)
                return BadRequest("Pessoa inválida.");

            var result = await _pessoaService.AlterarPessoaAsync(pessoaCommand, codigoPessoa);

			if (result == false)
				return BadRequest();
			else
				return Ok($"Resultado: {result}\nPessoa alterada com sucesso!");
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
