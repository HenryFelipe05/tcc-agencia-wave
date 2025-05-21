using Wave.Application.Services.Interfaces;
using Wave.Domain.Entities;
using Wave.Domain.Repositories;

namespace Wave.Application.Services
{
    public class FavoritoService : IFavoritoService
    {
        private readonly IFavoritoRepository _favoritoRepository;
        private readonly IItemGaleriaRepository _itemGaleriaRepository;

        public FavoritoService(IFavoritoRepository favoritoRepository, IItemGaleriaRepository itemGaleriaRepository)
        {
            _favoritoRepository = favoritoRepository;
            _itemGaleriaRepository = itemGaleriaRepository;
        }

        public async Task<Favorito> AdicionarFavoritoAsync(int codigoUsuario, int codigoItemGaleria)
        {
            // Verifica se o item existe
            var item = await _itemGaleriaRepository.ObterPorIdAsync(codigoItemGaleria);
            if (item == null)
                throw new InvalidOperationException("Item da galeria não encontrado.");

            // Verifica se já é favorito
            var favoritoExistente = await _favoritoRepository.BuscarFavoritoAsync(codigoUsuario, codigoItemGaleria);
            if (favoritoExistente != null)
                throw new InvalidOperationException("Este item já está nos favoritos.");

            var favorito = new Favorito
            {
                CodigoUsuario = codigoUsuario,
                CodigoItemGaleria = codigoItemGaleria,
                DataFavorito = DateTime.UtcNow
            };

            return await _favoritoRepository.AdicionarAsync(favorito);
        }


        public async Task<IEnumerable<Favorito>> ListarFavoritosAsync(int codigoUsuario)
        {
            var itens =  await _favoritoRepository.ListarFavoritosPorUsuarioAsync(codigoUsuario);

            if (itens == null || !itens.Any())
                throw new Exception("Você não possui itens favoritados");

            return itens;
        }

        public async Task RemoverFavoritoAsync(int codigoUsuario, int codigoItemGaleria)
        {
            var favorito = await _favoritoRepository.BuscarFavoritoAsync(codigoUsuario, codigoItemGaleria);
            if (favorito == null)
                throw new InvalidOperationException("Favorito não encontrado.");

            await _favoritoRepository.RemoverAsync(favorito.CodigoFavorito);
        }
    }
}
