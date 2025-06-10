using Wave.Domain.Entities;
using Wave.Domain.Commands;
using Wave.Domain.Queries;

namespace Wave.Application.Services.Interfaces
{
    public interface IGaleriaService
    {
        Task<ItemGaleria> CriarItemGaleriaAsync(CriarItemGaleriaCommand command);
        Task AlterarItemAsync(ItemGaleriaCommand command);
        Task<IEnumerable<ItemGaleria>> BuscarItensAsync(ItemGaleriaQuery query);
        Task<ItemGaleriaCommand> ObterItemAsync(int codigoItemGaleria);
        Task<string> BaixarItemAsync(ItemGaleriaCommand itemGaleriaCommand);
        Task<ItemGaleria> ExcluirItemAsync(int codigoItemGaleria, int codigoUsuario);
    }
}
