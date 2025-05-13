using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wave.Domain.Entities;

namespace Wave.Application.Services.Interfaces
{
    public interface IFavoritoService
    {
        Task<Favorito> AdicionarFavoritoAsync(int codigoUsuario, int codigoItemGaleria);
        Task RemoverFavoritoAsync(int codigoUsuario, int codigoItemGaleria);
        Task<IEnumerable<Favorito>> ListarFavoritosAsync(int codigoUsuario);
    }
}
