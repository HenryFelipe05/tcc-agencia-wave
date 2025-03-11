using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;
using Wave.Domain.Repository;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IUserStore<Usuario> _userStore;

        public UsuarioRepository(UserManager<Usuario> userManager, IUserStore<Usuario> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
        }

        public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userManager.CreateAsync(user, user.Senha);
        }


        public async Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = await _userManager.DeleteAsync(user);

            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedEmail))
            {
                return null;
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == normalizedEmail, cancellationToken);

            return user;
        }

        public async Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedUserName))
            {
                return null;
            }

            return await _userManager.FindByNameAsync(normalizedUserName);
        }

        public async Task<string> GetEmailAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return  await Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var normalizedEmail = user.Email?.Trim().ToLowerInvariant();

            return Task.FromResult(normalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var normalizedName = user.NomeUsuario.Trim().ToLowerInvariant();

            return Task.FromResult<string>(normalizedName);
        }

        public Task<string> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var password = user.Senha;

            var passwordHasher = new PasswordHasher<Usuario>();
            var hash = passwordHasher.HashPassword(user, password);

            return Task.FromResult(hash);
        }

        public Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.CodigoUsuario.ToString());
        }

        public Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NomeUsuario);
        }

        public Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            bool hasPassword = !string.IsNullOrEmpty(user.Senha);

            return Task.FromResult(hasPassword);
        }

        public async Task SetEmailAsync(Usuario user, string email, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("O email não pode ser nulo ou vazio", nameof(email));
            }

            user.Email = email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Falha ao atualizar o email do usuário");
            }
        }

            

        public async Task SetEmailConfirmedAsync(Usuario user, bool confirmed, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.EmailConfirmed = confirmed;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Falha ao atualizar a confirmação do email do usuário");
            }
        }

        public async Task SetNormalizedEmailAsync(Usuario user, string normalizedEmail, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(normalizedEmail))
            {
                throw new ArgumentException("O email normalizado não pode ser nulo ou vazio", nameof(normalizedEmail));
            }

            user.NormalizedEmail = normalizedEmail.ToLowerInvariant().Trim();

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Falha ao atualizar o email normalizado do usuário.");
            }
        }

        public async Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(normalizedName))
            {
                throw new ArgumentException("O nome normalizado nao pode ser nulo ou vazio", nameof (normalizedName));
            }

            user.NormalizedUserName = normalizedName.ToLowerInvariant().Trim();

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Falha ao atualizar o nome normalizado do usuário.");
            }
        }

        public async Task SetPasswordHashAsync(Usuario user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentException("O hash da senha nao pode ser nulo ou vazio.", nameof(passwordHash));
            }

            user.PasswordHash = passwordHash;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Falha ao atualizar o hash da senha do usuário.");
            }
        }

        public async Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("O nome do usuário nao pode ser nulo nem vazio.", nameof(userName));
            }

            user.UserName = userName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Falha ao atualizar o nome do usuário.");
            }
        }

        public async Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Falha ao atualizar o usuário.");
            }

            return result;
        }
    }
}
