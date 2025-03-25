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
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthenticationService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, JwtService tokenService,IUsuarioRepository usuarioRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = tokenService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IdentityResult> RegisterUserAsync(Usuario user,string password)
    {
        return await _userManager.CreateAsync(user,password);
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
