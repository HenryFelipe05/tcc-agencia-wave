using Wave.Domain.Entities;

namespace Wave.Domain.Repositories
{
    public interface IItemGaleriaRepository
    {
        Task<ItemGaleria> ObterPorIdAsync(int codigoItemGaleria);
        Task<IEnumerable<ItemGaleria>> ListarTodosAsync();
        Task<IEnumerable<ItemGaleria>> FiltrarAsync(string tipoArquivo, bool? exclusivo, string pesquisa);
        Task<ItemGaleria> CriarItemAsync(ItemGaleria itemGaleria);
        Task<ItemGaleria> AtualizarItemAsync(ItemGaleria itemGaleria);
        Task<ItemGaleria> DeletarItemAsync(ItemGaleria itemGaleria);
    }
}
