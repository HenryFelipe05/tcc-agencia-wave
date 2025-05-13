using Wave.Domain.Entities;

namespace Wave.Domain.Repositories
{
    public interface IFavoritoRepository
    {
        Task <Favorito> AdicionarAsync(Favorito favorito);
        Task RemoverAsync(int codigoFavorito);
        Task<Favorito> BuscarFavoritoAsync(int codigoUsuario, int codigoItemGaleria);
        Task<IEnumerable<Favorito>> ListarFavoritosPorUsuarioAsync(int codigoUsuario);
    }
}
