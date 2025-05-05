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

        public async Task<IEnumerable<UsuarioQuery>> RecuperarTodosUsuariosAsync()
        {
            return await _usuarioRepository.RecuperarTodosUsuariosAsync();
        }

        public async Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario)
        {
            return await _usuarioRepository.RecuperarUsuarioAsync(codigoUsuario);
        }
    }
}
