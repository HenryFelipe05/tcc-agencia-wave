using Microsoft.EntityFrameworkCore;
using Wave.Domain.Entities;
using Wave.Domain.Repository;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
    public class AssinaturaRepository : IAssinaturaRepository
    {
        private readonly WaveDbContext _context;

        public AssinaturaRepository(WaveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assinatura>> ObterPorUsuarioIdAsync(int CodigoUsuario)
        {
            return await _context.Assinaturas.Where(a => a.CodigoUsuario == CodigoUsuario).OrderByDescending(a => a.DataCadastro).ToListAsync();
        }

        public async Task<IEnumerable<Assinatura>> ObterTodasAsync()
        {
            return await _context.Assinaturas.ToListAsync();
        }

        public async Task<Assinatura> ObterPorIdAsync(Guid codigoAssinatura)
        {
            return await _context.Assinaturas.FindAsync(codigoAssinatura);
        }

        public async Task<Assinatura> CriarAssinaturaAsync(Assinatura assinatura)
        {
            _context.Assinaturas.Add(assinatura);
            await _context.SaveChangesAsync();
            return assinatura;
        }

        public async Task<Assinatura> AtualizarAssinaturaAsync(Assinatura assinatura)
        {
            var assinaturaExistente = await _context.Assinaturas.FindAsync(assinatura.CodigoAssinatura);
            if(assinatura is null)
                return null;

            _context.Entry(assinaturaExistente).CurrentValues.SetValues(assinatura);
            await _context.SaveChangesAsync();
            return assinaturaExistente;
        }

        public async Task<Assinatura> RemoverAssinaturaAsync(Guid codigoAssinatura)
        {
            var assinatura = await _context.Assinaturas.FindAsync(codigoAssinatura);
            if (assinatura is null)
                return null;

            _context.Assinaturas.Remove(assinatura);
            await _context.SaveChangesAsync();
            return assinatura;
        }
    }
}
