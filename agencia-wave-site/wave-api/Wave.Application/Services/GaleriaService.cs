using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;
using Wave.Domain.Repositories;
using Wave.Domain.Repository;

namespace Wave.Application.Services
{
    public class GaleriaService : IGaleriaService
    {
        private readonly IItemGaleriaRepository _itemRepository;
        private readonly IFavoritoRepository _favoritoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GaleriaService(IItemGaleriaRepository itemRepository, IFavoritoRepository favoriteRepository, IUsuarioRepository usuarioRepository)
        {
            _itemRepository = itemRepository;
            _favoritoRepository = favoriteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<byte[]> BaixarItemAsync(ItemGaleriaCommand Command)
        {
            var item = await _itemRepository.ObterPorIdAsync(Command.CodigoItemGaleria);
            if (item == null)
                throw new InvalidOperationException("Item da galeria não encontrado.");

            if (item.Arquivo == null || item.Arquivo.Length == 0)
                throw new InvalidOperationException("Arquivo não disponível para este item.");

            return item.Arquivo; // Agora retornamos o arquivo após verificar se não é nulo
        }

        
        public async Task<IEnumerable<ItemGaleria>> BuscarItensAsync(ItemGaleriaQuery query)
        {
            var item = await _itemRepository.ListarTodosAsync();

            if (item is null || !item.Any())
                throw new InvalidOperationException("Itens não encontrados");

            return item.ToList();
        }

        public Task ExcluirItemAsync(int codigoItemGaleria)
        {
            var itemExcluido = _itemRepository.DeletarAsync(codigoItemGaleria);

            if (itemExcluido is null)
                throw new InvalidOperationException("Item nao encontrado");

            return itemExcluido;
        }

        public async Task FavoritarItemAsync(int codigoItemGaleria, int codigoUsuario)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(codigoUsuario);

            if (usuario is null)
                throw new InvalidOperationException("Usuario não encontrado");

            var favorito = new Favorito
            {
                CodigoUsuario = codigoUsuario,
                CodigoItemGaleria = codigoItemGaleria,
                DataFavorito = DateTime.UtcNow
            };

            await _favoritoRepository.CriarAsync(favorito);
        }

        public async Task SalvarItemAsync(ItemGaleriaCommand command, int codigoUsuario)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(codigoUsuario);
            if (usuario == null || usuario.Perfil != "Suporte")
                throw new UnauthorizedAccessException("Apenas usuários com perfil de suporte podem gerenciar a galeria.");

            if (command.CodigoItemGaleria != 0)
            {
                var itemExistente = await _itemRepository.ObterPorIdAsync(command.CodigoItemGaleria);
                if (itemExistente == null)
                    throw new InvalidOperationException("Item não encontrado.");

                // Atualiza os dados do item
                itemExistente.Titulo = command.Titulo;
                itemExistente.Descricao = command.Descricao;
                itemExistente.ExtensaoArquivo = command.ExtensaoArquivo;
                itemExistente.Arquivo = !string.IsNullOrEmpty(command.ArquivoBase64)
                    ? Convert.FromBase64String(command.ArquivoBase64)
                    : itemExistente.Arquivo;
                itemExistente.URLMiniatura = command.URLMiniatura;
                itemExistente.Ativo = command.Ativo;
                itemExistente.CodigoGaleria = command.CodigoGaleria;

                await _itemRepository.AtualizarAsync(itemExistente);
            }
            else
            {
                var novoItem = new ItemGaleria
                {
                    Titulo = command.Titulo,
                    Descricao = command.Descricao,
                    ExtensaoArquivo = command.ExtensaoArquivo,
                    Arquivo = !string.IsNullOrEmpty(command.ArquivoBase64)
                        ? Convert.FromBase64String(command.ArquivoBase64)
                        : null,
                    URLMiniatura = command.URLMiniatura,
                    Ativo = command.Ativo,
                    CodigoGaleria = command.CodigoGaleria,
                    DataCadastro = DateTime.UtcNow
                };

                await _itemRepository.CriarAsync(novoItem);
            }
        }
    }
}
