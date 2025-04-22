using Wave.Domain.Entities;

namespace Wave.Domain.Repository
{
    public interface IAssinaturaRepository
    {
        Task<IEnumerable<Assinatura>> ObterTodasAsync();
        Task<Assinatura> ObterPorIdAsync(Guid CodigoAssinatura);
        Task<Assinatura> CriarAssinaturaAsync(Assinatura assinatura);
        Task<Assinatura> AtualizarAssinaturaAsync(Assinatura assinatura);
        Task<Assinatura> RemoverAssinaturaAsync(Guid CodigoAssinatura);
        
    }
}
