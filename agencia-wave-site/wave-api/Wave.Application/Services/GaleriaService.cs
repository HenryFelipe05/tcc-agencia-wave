using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;
using Wave.Domain.Repositories;
using Wave.Domain.Repository;

namespace Wave.Application.Services
{
    internal class GaleriaService : IGaleriaService
    {
        private readonly IItemGaleriaRepository _itemRepository;
        private readonly IFavoritoRepository _favoriteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GaleriaService(IItemGaleriaRepository itemRepository, IFavoritoRepository favoriteRepository, IUsuarioRepository usuarioRepository)
        {
            _itemRepository = itemRepository;
            _favoriteRepository = favoriteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public Task<byte[]> BaixarItemAsync(int codigoItemGaleria)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemGaleria>> BuscarItensAsync(ItemGaleriaQuery query)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirItemAsync(int codigoItemGaleria)
        {
            throw new NotImplementedException();
        }

        public Task FavoritarItemAsync(int codigoItemGaleria)
        {
            throw new NotImplementedException();
        }


        public async Task SalvarItemAsync(ItemGaleriaCommand command, int usuarioId)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(usuarioId);

            if (usuario == null || usuario.Perfil != "Suporte")
                throw new UnauthorizedAccessException("Apenas usuários com perfil de suporte podem gerenciar a galeria.");

            if (command.CodigoItemGaleria == null )
            {
                var existente = await _itemRepository.ObterPorIdAsync(command.CodigoItemGaleria);
                if (existente == null) throw new Exception("Item não encontrado.");

                existente.Titulo = command.Titulo;
                existente.Descricao = command.Descricao;
                existente.ExtensaoArquivo = command.ExtensaoArquivo;
                existente.Arquivo = command.Arquivo;
                existente.URLMiniatura = command.URLMiniatura;
                existente.Ativo = command.Ativo;
                existente.CodigoGaleria = command.CodigoGaleria;

                await _itemRepository.AtualizarAsync(existente);
            }
            else
            {
                var novo = new ItemGaleria
                {
                    Titulo = command.Titulo,
                    Descricao = command.Descricao,
                    ExtensaoArquivo = command.ExtensaoArquivo,
                    Arquivo = command.Arquivo,
                    URLMiniatura = command.URLMiniatura,
                    Ativo = command.Ativo,
                    CodigoGaleria = command.CodigoGaleria,
                    DataCadastro = DateTime.UtcNow
                };

                await _itemRepository.CriarAsync(novo);
            }
        }
    }
}
