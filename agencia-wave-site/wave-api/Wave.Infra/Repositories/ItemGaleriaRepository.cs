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
            var itens = await _context.ItemGalerias.ToListAsync();
            if (itens == null || !itens.Any())
            {
                // Verifica se o retorno está vazio
                Console.WriteLine("Nenhum item encontrado.");
            }
            return itens;
        }


        public async Task<ItemGaleria> ObterPorIdAsync(int codigoItemGaleria)
        {
            if (codigoItemGaleria == 0)
                throw new Exception("Erroooooo");
                
            return await _context.ItemGalerias.FindAsync(codigoItemGaleria);
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
            _context.ItemGalerias.Update(itemGaleria);
            await _context.SaveChangesAsync();

            return itemGaleria;
        }

        public async Task<ItemGaleria> DeletarItemAsync(ItemGaleria itemGaleria)
        {
     

            _context.ItemGalerias.Remove(itemGaleria);
            await _context.SaveChangesAsync();

            return itemGaleria;
        }

    }
}
