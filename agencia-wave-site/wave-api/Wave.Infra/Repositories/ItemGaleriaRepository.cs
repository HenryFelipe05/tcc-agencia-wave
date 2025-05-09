using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Repositories;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
    public class ItemGaleriaRepository : IItemGaleriaRepository
    {
        private readonly WaveDbContext _context;

        public ItemGaleriaRepository(WaveDbContext context)
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

        public async Task<ItemGaleria> CriarAsync(ItemGaleria itemGaleria)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("O contexto do banco de dados não foi inicializado.");
            }

            _context.ItemGalerias.Add(itemGaleria);
            await _context.SaveChangesAsync();

            return itemGaleria;
        }

        public async Task<ItemGaleria> AtualizarAsync(ItemGaleria itemGaleria)
        {
           _context.ItemGalerias.Update(itemGaleria);
            await _context.SaveChangesAsync();

            return itemGaleria;
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
