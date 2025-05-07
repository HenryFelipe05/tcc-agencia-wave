using Wave.Domain.Commands;
using Wave.Domain.Entities;

namespace Wave.Domain.Repositories
{
    public interface IItemGaleriaRepository
    {
        Task<ItemGaleria> ObterPorIdAsync(int codigoItemGaleria);
        Task<IEnumerable<ItemGaleria>> ListarTodosAsync();
        Task<IEnumerable<ItemGaleria>> FiltrarAsync(string tipoArquivo, bool? exclusivo, string pesquisa);
        Task <ItemGaleria> CriarAsync(ItemGaleria itemGaleria);
        Task AtualizarAsync(ItemGaleria itemGaleria);
        Task DeletarAsync(int codigoItemGaleria);
    }
}
