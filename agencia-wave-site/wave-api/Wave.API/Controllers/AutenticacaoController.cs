using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPessoaService _pessoaService;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public AutenticacaoController(IUsuarioService usuarioService,
                                      IPessoaService pessoaService,
                                      IJwtService jwtService,
                                      IPasswordHasher passwordHasher)
        {
            _usuarioService = usuarioService;
            _pessoaService = pessoaService;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;

        }


        [HttpPost("registrarUsuario/Pessoa")]
        public async Task<ActionResult<UsuarioQuery>> RegistrarUsuario([FromBody] RegistrarUsuarioCommand registrarUsuarioCommand)
        {
            try
            {
                var pessoa = Pessoa.MapearCommandPessoa
                (
                    registrarUsuarioCommand.Nome,
                    registrarUsuarioCommand.Sobrenome,
                    registrarUsuarioCommand.DataNascimento,
                    registrarUsuarioCommand.CodigoGenero
                 );

                var pessoaAdicionada = await _pessoaService.AdicionarPessoaAsync(pessoa);

                if (pessoaAdicionada == null)
                    return BadRequest("Erro ao adicionar a pessoa.");

                var usuarioCommand = new RegistrarUsuarioCommand
                {
                    CodigoPessoa = pessoaAdicionada.CodigoPessoa,
                    NomeUsuario = registrarUsuarioCommand.NomeUsuario,
                    Email = registrarUsuarioCommand.Email,
                    Telefone = registrarUsuarioCommand.Telefone,
                    Senha = registrarUsuarioCommand.Senha,
                    SenhaConfirmada = registrarUsuarioCommand.SenhaConfirmada,
                    Ativo = registrarUsuarioCommand.Ativo,
                    CodigoPerfil = registrarUsuarioCommand.CodigoPerfil
                };

                var usuarioAdicionado = await _usuarioService.AdicionarUsuarioAsync(usuarioCommand);

                if (usuarioAdicionado is null)
                    return BadRequest("Erro ao adicionar o usuário.");

                return Ok(usuarioAdicionado);
            }
            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro inesperado. Contate o suporte.");
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var usuarioExistente = await _usuarioService.BuscarUsuarioPorNomeOuEmailAsync(loginUserCommand.UserName);

            if (usuarioExistente == null)
                return Unauthorized("Usuário não encontrado");

            bool senhaValida = _passwordHasher.VerifyPassword(loginUserCommand.Password, usuarioExistente.Senha);
            if (!senhaValida)
                return Unauthorized("Senha inválida");

            var token = _jwtService.GenerateToken(usuarioExistente.CodigoUsuario.ToString(), usuarioExistente.Perfil?.Descricao ?? "Usuário");

            return Ok(new { token });
        }
    }
}
