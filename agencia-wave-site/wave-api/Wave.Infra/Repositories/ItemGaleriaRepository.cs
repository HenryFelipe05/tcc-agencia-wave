using Microsoft.AspNetCore.Http.HttpResults;
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
            var itens = await _context.ItemGalerias.AsNoTracking().ToListAsync();

            if (itens == null || itens.Count == 0)
            {
                // Verifica se o retorno está vazio
                Console.WriteLine("Nenhum item encontrado.");
            }
            return itens;
        }


        public async Task<ItemGaleria> ObterPorIdAsync(int codigoItemGaleria)
        {
            if (codigoItemGaleria <= 0)
                throw new ArgumentException("O código do item deve ser maior que zero.", nameof(codigoItemGaleria));

            var item = await _context.ItemGalerias
                .FirstOrDefaultAsync(x => x.CodigoItemGaleria == codigoItemGaleria);

            if (item == null)
                throw new KeyNotFoundException($"Item com código {codigoItemGaleria} não foi encontrado.");

            return item;
        }


        public async Task<ItemGaleria> CriarItemAsync(ItemGaleria itemGaleria)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("O contexto do banco de dados não foi inicializado.");
            }

            try
            {
                _context.ItemGalerias.Add(itemGaleria);
                await _context.SaveChangesAsync();

                return itemGaleria;
            }
            catch
            {

                throw new Exception($"Erro ao salvar: {itemGaleria}");
            }

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

        public async Task<ItemGaleria> AtualizarItemAsync(ItemGaleria itemGaleria)
        {
            await _context.SaveChangesAsync();
            return itemGaleria;
        }


        public async Task<ItemGaleria> DeletarItemAsync(int codigoItemGaleria)
        {
            var itemExcluir = await _context.ItemGalerias.FindAsync(codigoItemGaleria);

            if (itemExcluir == null)
                throw new InvalidOperationException("Item não encontrado.");

            _context.ItemGalerias.Remove(itemExcluir);
            await _context.SaveChangesAsync();

            return itemExcluir;
        }

    }
}
