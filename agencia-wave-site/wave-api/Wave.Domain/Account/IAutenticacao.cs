using Wave.Domain.Entities;

namespace Wave.Domain.Account
{
    public interface IAutenticacao
    {
        Task<bool> UsuarioExiste(string email);
        Task<bool> AutenticarAsync(string email, string senha);
        Task<Usuario> RecuperarUsuarioPorEmail(string email);
        public string GerarToken(int codigoUsuario, string email);
    }
}
