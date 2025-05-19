using Wave.Domain.Commands;
using Wave.Domain.Entities;

namespace Wave.Application.Services.Interfaces
{
    public interface IAssinaturaService
    {
        Task<IEnumerable<Assinatura>> ObterTodasAsync();
        Task<Assinatura> ObterPorIdAsync(Guid CodigoAssinatura);
        Task<Assinatura> CriarAssinaturaAsync(AssinaturaCommand assinaturaCommand);
        Task<Assinatura> AtualizarAssinaturaAsync(AssinaturaCommand assinaturaCommand);
        Task<Assinatura> RemoverAssinaturaAsync(Guid CodigoAssinatura);
        Task<IEnumerable<Assinatura>> ObterPorUsuarioIdAsync(int CodigoUsuario);
        Task<IEnumerable<Assinatura>> ObterHistoricoDeAssinaturasAsync(int codigoUsuario);
        Task<string> CancelarAssinaturaAsync(Guid codigoAssinatura);
    }
}
