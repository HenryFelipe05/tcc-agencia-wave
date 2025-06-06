using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;

namespace Wave.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario);
        Task<IEnumerable<UsuarioQuery>> RecuperarTodosUsuariosAsync();
        Task<UsuarioQuery> AdicionarUsuarioAsync(RegistrarUsuarioCommand command);
        Task AtualizarUsuarioAsync(UsuarioCommand usuarioCommand, int codigoUsuario);
        Task<Usuario> BuscarUsuarioPorNomeOuEmailAsync(string identificador);
    }
}
