using Microsoft.AspNetCore.Identity;
using Wave.Domain.Entities;

namespace Wave.Application.Services
{
    internal class AuthenticationService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AuthenticationService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(Usuario user, string password, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<SignInResult> LoginUserAsync(string userName, string password, bool rememberMe, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
            return result;
        }

        public async Task LogoutUserAsync(CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
        }

    }
}
