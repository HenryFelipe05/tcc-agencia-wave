using Microsoft.EntityFrameworkCore;
using Wave.Domain.Entities;
using Wave.Domain.Repositories;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
    public class ItemGaleriaRepesitory : IItemGaleriaRepository
    {
        private readonly WaveDbContext _context;

        public ItemGaleriaRepesitory(WaveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemGaleria>> ListarTodosAsync()
        {
            return await _context.ItemGalerias.AsNoTracking().ToListAsync();
        }

        public async Task<ItemGaleria> ObterPorIdAsync(int codigoItemGaleria)
        {
            return await _context.ItemGalerias.FirstOrDefaultAsync(x => x.CodigoItemGaleria == codigoItemGaleria);
        }

        public async Task<IEnumerable<ItemGaleria>> FiltrarAsync(string tipoArquivo, bool? exclusivo, string pesquisa)
        {
            var query = _context.ItemGalerias.AsQueryable();

            if (!string.IsNullOrEmpty(tipoArquivo))
                query = query.Where(x => x.ExtensaoArquivo == tipoArquivo);

            if (exclusivo.HasValue)
            {
                query = exclusivo.Value
                    ? query.Where(x => x.CodigoGaleria == 2)
                    : query.Where(x => x.CodigoGaleria == 1);
            }

            if (!string.IsNullOrEmpty(pesquisa))
                query = query.Where(x => x.Titulo.Contains(pesquisa) || x.Descricao.Contains(pesquisa));

            return await query.ToListAsync();
        }

        public async Task CriarAsync(ItemGaleria itemGaleria)
        {
            await _context.ItemGalerias.AddAsync(itemGaleria);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ItemGaleria itemGaleria)
        {
           _context.ItemGalerias.Update(itemGaleria);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int codigoItemGaleria)
        {
            var item = await ObterPorIdAsync(codigoItemGaleria);
            if(item != null)
            {
                _context.ItemGalerias.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
