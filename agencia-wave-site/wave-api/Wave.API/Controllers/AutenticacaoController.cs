using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Enums;
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

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioQuery>> RegistrarUsuario([FromBody] RegistrarUsuarioCommand registrarUsuarioCommand)
        {
            var pessoaCommand = Pessoa.MapearCommandPessoa
            (
                registrarUsuarioCommand.Nome,
                registrarUsuarioCommand.Sobrenome,
                registrarUsuarioCommand.DataNascimento,
                registrarUsuarioCommand.CodigoGenero
            );
   
            var usuarioCommand = Usuario.MapearCommandUsuario
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

            var pessoaAdicionada = await _pessoaService.AdicionarPessoaAsync(pessoaCommand);

            if (pessoaAdicionada)
            {
                var listaPessoas = await _pessoaService.RecuperarListaPessoasAsync();
                var ultimaPessoa = listaPessoas.OrderByDescending(p => p.CodigoPessoa).FirstOrDefault();

                if (ultimaPessoa != null)
                {
                    usuarioCommand.CodigoPessoa = ultimaPessoa.CodigoPessoa;
                    usuarioCommand.CodigoPerfil = (int)PerfilEnum.Perfis.Usuario;
                    usuarioCommand.Ativo = true;

                    var usuarioAdicionado = await _usuarioService.AdicionarUsuarioAsync(usuarioCommand);
                    // Autenticação e autorização 
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Erro ao recuperar o código da pessoa adicionada.");
                }
            }
            else
            {
                return BadRequest("Erro ao adicionar a pessoa.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var usuarioExistente = await _usuarioService.BuscarUsuarioPorNomeOuEmailAsync(loginUserCommand.UserName);

            if(usuarioExistente == null)
                return Unauthorized("Usuário não encontrado");

            if(usuarioExistente.Senha != loginUserCommand.Password)
                return Unauthorized("Senha incorreta");

            var token = _jwtService.GenerateToken(usuarioExistente.CodigoUsuario.ToString(), usuarioExistente.Perfil?.Descricao ?? "Usuário");

            return Ok(new { token });
        }
    }
}
