using Microsoft.AspNetCore.Identity;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Entities; // Importando a classe Usuario

namespace Wave.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<Usuario> _userManager; // Usando Usuario
        private readonly SignInManager<Usuario> _signInManager; // Usando Usuario
        private readonly JwtService _jwtService;

        public LoginService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var usuario = await _userManager.FindByEmailAsync(email); // Procurando o usuario do tipo Usuario
            if (usuario == null)
                throw new UnauthorizedAccessException("Email ou senha incorretos.");

            var result = await _signInManager.CheckPasswordSignInAsync(usuario, password, false); // Verificando a senha
            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Email ou senha incorretos.");

            // Gerando o token JWT
            return _jwtService.GenerateToken(usuario);
        }
    }
}
