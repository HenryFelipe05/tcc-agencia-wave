using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Application.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Wave.Domain.Entities;
using Microsoft.AspNetCore.Authentication;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly AuthenticationService _authenticationService; // Serviço de autenticação
        private readonly JwtService _jwtService; // Serviço para gerar JWT
        private readonly UserManager<Usuario> _userManager; // Para gerenciar usuários com Identity

        public AutenticacaoController(IPessoaService pessoaService,
                                      AuthenticationService authenticationService,
                                      JwtService jwtService,
                                      UserManager<Usuario> userManager)
        {
            _pessoaService = pessoaService;
            _authenticationService = authenticationService;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        // Método para registrar um novo usuário
        [HttpPost("registrar")]
        public async Task<ActionResult> AdicionarNovoUsuario([FromBody] NovoUsuarioCommand novoUsuarioCommand)
        {
            // Adiciona a pessoa (caso necessário)
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

            // Agora, crie o usuário e adicione ele ao sistema
            var usuario = new Usuario
            {
                UserName = novoUsuarioCommand.NomeUsuario,
                Email = novoUsuarioCommand.Email,
                
            };

            var resultado = await _authenticationService.RegisterUserAsync(usuario, novoUsuarioCommand.Senha);

            if (!resultado.Succeeded)
            {
                return BadRequest(new { mensagem = "Erro ao registrar usuário", detalhes = resultado.Errors });
            }

            return Ok(new { mensagem = "Usuário registrado com sucesso!" });
        }

        // Método para login (gerar token)
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            // Verifica se o usuário e a senha são válidos
            var token = await _authenticationService.LoginUserAsync(loginCommand.UserName, loginCommand.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { mensagem = "Credenciais inválidas!" });
            }

            // Retorna o token JWT gerado
            return Ok(new { message = "Login bem-sucedido", token });
        }

        // Método para logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutUserAsync();
            return Ok(new { mensagem = "Logout bem-sucedido!" });
        }
    }
}
