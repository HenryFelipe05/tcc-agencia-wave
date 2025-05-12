using Wave.Domain.Entities;
using Wave.Domain.Commands;
using Wave.Domain.Queries;

namespace Wave.Application.Services
{
    public interface IGaleriaService
    {
        Task <ItemGaleria>CriarItemGaleriaAsync(CriarItemGaleriaCommand command);
        Task AlterarItemAsync(ItemGaleria itemGaleria, int codigoUsuario);
        Task<IEnumerable<ItemGaleria>> BuscarItensAsync(ItemGaleriaQuery query);
        Task FavoritarItemAsync(int codigoItemGaleria, int codigoUsuario);
        Task<byte[]> BaixarItemAsync(ItemGaleriaCommand itemGaleriaCommand);
        Task<ItemGaleria> ExcluirItemAsync(ExcluirItemGaleriaCommand command);
    }
}
