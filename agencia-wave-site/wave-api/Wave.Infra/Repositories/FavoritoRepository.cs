using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Task CriarAsync(Favorito favorito)
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsync(int codigoFavorito)
        {
            throw new NotImplementedException();
        }
    }
}
