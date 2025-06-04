using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPessoaService _pessoaService;
        private readonly IJwtService _jwtService;

        public AutenticacaoController(IUsuarioService usuarioService,
                                      IPessoaService pessoaService,
                                      IJwtService jwtService)
        {
            _usuarioService = usuarioService;
            _pessoaService = pessoaService;
            _jwtService = jwtService;
        }

        [HttpPost("registrarPessoa")]
        public async Task<ActionResult<PessoaQuery>> RegistrarPessoa([FromBody] PessoaCommand pessoaCommand)
        {
            var pessoa = Pessoa.MapearCommandPessoa
            (
                pessoaCommand.Nome,
                pessoaCommand.Sobrenome,
                pessoaCommand.DataNascimento,
                pessoaCommand.CodigoGenero
            );

            var pessoaAdicionada = await _pessoaService.AdicionarPessoaAsync(pessoa);

            if (pessoaAdicionada == null)
                return BadRequest("Erro ao adicionar a pessoa.");

            return Ok(pessoaAdicionada);
        }

        [HttpPost("registrarUsuario")]
        public async Task<ActionResult<UsuarioQuery>> RegistrarUsuario([FromBody] RegistrarUsuarioCommand registrarUsuarioCommand)
        {
            var usuario = Usuario.MapearCommandUsuario
                (
                    registrarUsuarioCommand.CodigoPessoa,
                    registrarUsuarioCommand.NomeUsuario,
                    registrarUsuarioCommand.Email,
                    registrarUsuarioCommand.Telefone,
                    registrarUsuarioCommand.Senha,
                    registrarUsuarioCommand.SenhaConfirmada,
                    registrarUsuarioCommand.Ativo,
                    registrarUsuarioCommand.CodigoPerfil 
                    );

            var usuarioAdicionado = await _usuarioService.AdicionarUsuarioAsync(usuario);

            if (usuarioAdicionado is null)
                return BadRequest("Erro ao adicionar o usuario");

            return Ok(usuarioAdicionado);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var usuarioExistente = await _usuarioService.BuscarUsuarioPorNomeOuEmailAsync(loginUserCommand.UserName);

            if (usuarioExistente == null)
                return Unauthorized("Usuário não encontrado");

            if (usuarioExistente.Senha != loginUserCommand.Password)
                return Unauthorized("Senha incorreta");

            var token = _jwtService.GenerateToken(usuarioExistente.CodigoUsuario.ToString(), usuarioExistente.Perfil?.Descricao ?? "Usuário");

            return Ok(new { token });
        }
    }
}
