using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;
using Wave.Domain.Repository;

namespace Wave.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPessoaService _pessoaService;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPessoaService pessoaService, IPasswordHasher passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _pessoaService = pessoaService;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioQuery> AdicionarUsuarioAsync(RegistrarUsuarioCommand command)
        {
            if (command.CodigoPessoa == 0)
                throw new ValidacaoException("Código da pessoa não pode ser zero.");

            if (command.Senha != command.SenhaConfirmada)
                throw new ValidacaoException("A senha e a confirmação não coincidem.");

            var pessoa = await _pessoaService.RecuperarPessoaAsync(command.CodigoPessoa);
            if (pessoa == null)
                throw new ValidacaoException("Código da pessoa inválido.");

            string senhaHash = _passwordHasher.HashPassword(command.Senha);

            var usuario = Usuario.MapearCommandUsuario
            (
                command.CodigoPessoa,
                command.NomeUsuario,
                command.Email,
                command.Telefone,
                senhaHash,
                command.Ativo,
                command.CodigoPerfil
                );

            return await _usuarioRepository.AdicionarUsuarioAsync(usuario);
        }

        public async Task AtualizarUsuarioAsync(UsuarioCommand usuarioCommand, int codigoUsuario)
        {
            await _usuarioRepository.AtualizarUsuarioAsync(usuarioCommand, codigoUsuario);
        }

        public async Task<Usuario> BuscarUsuarioPorNomeOuEmailAsync(string identificador)
        {
            return await _usuarioRepository.BuscarUsuarioPorNomeOuEmailAsync(identificador);
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
