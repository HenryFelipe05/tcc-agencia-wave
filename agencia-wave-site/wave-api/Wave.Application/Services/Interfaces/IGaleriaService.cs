using Wave.Domain.Entities;
using Wave.Domain.Commands;
using Wave.Domain.Queries;

namespace Wave.Application.Services
{
    public interface IGaleriaService
    {
        Task SalvarItemAsync(ItemGaleriaCommand command);
        Task<IEnumerable<ItemGaleria>> BuscarItensAsync(ItemGaleriaQuery query);
        Task FavoritarItemAsync(int codigoItemGaleria);
        Task<byte[]> BaixarItemAsync(int codigoItemGaleria);
        Task ExcluirItemAsync(int codigoItemGaleria);
    }
}
