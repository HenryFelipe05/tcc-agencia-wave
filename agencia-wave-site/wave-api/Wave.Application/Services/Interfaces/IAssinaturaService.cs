using Wave.Domain.Entities;

namespace Wave.Application.Services.Interfaces
{
    public interface IAssinaturaService
    {
        Task<IEnumerable<Assinatura>> ObterTodasAsync();
        Task<Assinatura> ObterPorIdAsync(int CodigoAssinatura);
        Task<Assinatura> CriarAssinaturaAsync(Assinatura assinatura);
        Task<Assinatura> AtualizarAssinaturaAsync(Assinatura assinatura);
        Task<Assinatura> RemoverAssinaturaAsync(int CodigoAssinatura);
    }
}
