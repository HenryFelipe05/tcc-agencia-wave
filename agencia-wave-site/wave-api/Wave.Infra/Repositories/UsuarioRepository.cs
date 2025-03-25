
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wave.Domain.Entities;
using Wave.Domain.Repository;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly WaveDbContext _waveDbContext;

       public UsuarioRepository(WaveDbContext waveDbContext)
        {
            _waveDbContext = waveDbContext;
        }
       

        public  async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _waveDbContext.AddAsync(user);
            return IdentityResult.Success;
        }


        public async Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

             _waveDbContext.Remove(user);

            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<Usuario> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedEmail))
            {
                return null;
            }

            var user = await _waveDbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == normalizedEmail, cancellationToken);

            return user;
        }

        public  Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

             return _waveDbContext.FindAsync<Usuario>(userId).AsTask();
            
        }

        public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedUserName))
            {
                return null;
            }

            return await _waveDbContext.Usuarios
                                       .SingleOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
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

            var passwordHasher = new PasswordHasher<Usuario>(); //BCrypt futuramente
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

            _waveDbContext.Usuarios.Update(user);

            var result = await _waveDbContext.SaveChangesAsync(cancellationToken);

            if (result == 0)
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

            _waveDbContext.Usuarios.Update(user);

            var result = await _waveDbContext.SaveChangesAsync(cancellationToken);

            if (result == 0)
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

            _waveDbContext.Usuarios.Update(user);

            var result = await _waveDbContext.SaveChangesAsync(cancellationToken);

            if (result == 0)
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

            _waveDbContext?.Usuarios.Update(user);

            var result = await _waveDbContext.SaveChangesAsync(cancellationToken);

            if (result == 0)
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

            _waveDbContext.Usuarios.Update(user);

            var result = await _waveDbContext.SaveChangesAsync(cancellationToken);

            if (result == 0)
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

            _waveDbContext?.Usuarios.Update(user);

            var result = await _waveDbContext.SaveChangesAsync(cancellationToken);

            if (result == 0)
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

            _waveDbContext.Usuarios.Update(user);

            var result = await _waveDbContext.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Falha ao atualizar o usuário."
                });
            }

            return IdentityResult.Success;
        }

    }
}
