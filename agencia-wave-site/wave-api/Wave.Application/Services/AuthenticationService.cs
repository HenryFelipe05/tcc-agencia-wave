using Microsoft.AspNetCore.Identity;
using Wave.Application.Services;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Entities;
using Wave.Domain.Repository;

public class AuthenticationService : IAutenticationService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly JwtService _jwtService;

    public AuthenticationService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, JwtService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = tokenService;
    }

    public async Task<IdentityResult> RegisterUserAsync(Usuario user, string password)
    {
        var usuario = Activator.CreateInstance<Usuario>();
        usuario.UserName = user.UserName;
        usuario.NormalizedUserName = user.UserName.ToUpper();
        usuario.Email = user.Email;
        usuario.NormalizedEmail = user.Email.ToUpper();

        var result = await _userManager.CreateAsync(usuario);

        if (result.Succeeded)
        {
            // Após a criação do usuário, adicionar a senha
            result = await _userManager.AddPasswordAsync(user, password);
        }

        return result;
    }

    public async Task<string> LoginUserAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
        {
            return null; 
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (result.Succeeded)
        {
            return _jwtService.GenerateToken(user); 
        }

        return null; 
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
