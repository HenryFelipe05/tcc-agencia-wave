using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wave.Domain.Commands;
using Wave.Domain.Queries;

namespace Wave.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario);
        Task<IEnumerable<UsuarioQuery>> RecuperarTodosUsuariosAsync();
        Task<UsuarioQuery> AdicionarUsuarioAsync(UsuarioCommand usuarioCommand);
        Task AtualizarUsuarioAsync(UsuarioCommand usuarioCommand, int codigoUsuario);
    }
}
