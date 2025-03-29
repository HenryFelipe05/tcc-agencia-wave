using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Queries;
using Wave.Domain.Repository;

namespace Wave.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioQuery> AdicionarUsuarioAsync(UsuarioCommand usuarioCommand)
        {
            return await _usuarioRepository.AdicionarUsuarioAsync(usuarioCommand);
        }

        public Task AtualizarUsuarioAsync(UsuarioCommand usuarioCommand, int codigoUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioQuery>> RecuperarTodosUsuariosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
