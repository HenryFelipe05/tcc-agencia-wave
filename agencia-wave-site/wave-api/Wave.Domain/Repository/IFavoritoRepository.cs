using Wave.Domain.Entities;

namespace Wave.Domain.Repositories
{
    public interface IFavoritoRepository
    {
        Task CriarAsync(Favorito favorito);
        Task RemoverAsync(int codigoFavorito);
        Task<IEnumerable<Favorito>> ListarFavoritosPorUsuarioAsync(int codigoUsuario);
    }
}
