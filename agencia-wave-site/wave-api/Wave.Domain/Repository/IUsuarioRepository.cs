using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;

namespace Wave.Domain.Repository
{
    public interface IUsuarioRepository
    {
        Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario);
        Task<IEnumerable<UsuarioQuery>> RecuperarTodosUsuariosAsync();
        Task<UsuarioQuery> AdicionarUsuarioAsync(UsuarioCommand usuarioCommand);
        Task AtualizarUsuarioAsync(UsuarioCommand usuarioCommand, int codigoUsuario);
        Task<Usuario> BuscarUsuarioPorNomeOuEmailAsync(string identificador);
    }
}
