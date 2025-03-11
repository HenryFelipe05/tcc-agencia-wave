using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;

namespace Wave.Domain.Repository
{
    public interface IUsuarioRepository : IUserPasswordStore<Usuario>, IUserEmailStore<Usuario>
    {
        //Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario);
        //Task<IEnumerable<UsuarioQuery>> RecuperarListaUsuariosAsync();
        //Task<bool> AdicionarUsuarioAsync(UsuarioCommand usuarioCommand);
        //Task<bool> InativarUsuarioAsync(int codigoUsuario);
        //Task<bool> AlterarSenhaUsuarioAsync(int codigoUsuario, string senha);
        //Task<bool> AlterarPerfilUsuarioAsync(int codigoUsuario, int codigoPerfil);
        //Task<bool> DeletarUsuarioAsync(int codigoUsuario);
        Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken);
    }
}
