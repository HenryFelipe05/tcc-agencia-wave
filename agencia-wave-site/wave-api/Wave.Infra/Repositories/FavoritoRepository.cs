using Microsoft.EntityFrameworkCore;
using Wave.Domain.Entities;
using Wave.Domain.Repositories;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
    internal class FavoritoRepository : IFavoritoRepository
    {
        private readonly WaveDbContext _context;

        public FavoritoRepository(WaveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorito>> ListarFavoritosPorUsuarioAsync(int codigoUsuario)
        {
            return await _context.Favoritos
                .AsNoTracking()
                .Where(x => x.CodigoUsuario == codigoUsuario)
                .ToListAsync();
        }

        public async Task CriarAsync(Favorito favorito)
        {
            var favoritoExistente = await _context.Favoritos
                .FirstOrDefaultAsync(x => x.CodigoUsuario == favorito.CodigoUsuario && x.CodigoItemGaleria == favorito.CodigoItemGaleria);

            if (favoritoExistente != null)
            {
                throw new InvalidOperationException("Este item já foi favoritado.");
            }

            await _context.Favoritos.AddAsync(favorito);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int codigoFavorito)
        {
            var favorito = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.CodigoFavorito == codigoFavorito);

            if (favorito == null)
            {
                throw new InvalidOperationException("Favorito não encontrado.");
            }

            _context.Favoritos.Remove(favorito);

            await _context.SaveChangesAsync();
        }

    }
}
