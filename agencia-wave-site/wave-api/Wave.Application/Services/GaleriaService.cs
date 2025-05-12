using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;
using Wave.Domain.Repositories;
using Wave.Domain.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public async Task<ItemGaleria> CriarItemGaleriaAsync(CriarItemGaleriaCommand command)
        {
            // Verifique se as propriedades essenciais não estão nulas
            if (string.IsNullOrEmpty(command.Titulo) || string.IsNullOrEmpty(command.Descricao))
            {
                throw new ArgumentException("Título e descrição são obrigatórios.");
            }

            if (command.Arquivo == null || command.Arquivo.Length == 0)
            {
                throw new ArgumentException("Arquivo não pode ser nulo ou vazio.");
            }

            var itemGaleria = new ItemGaleria
            {
                Titulo = command.Titulo,
                Descricao = command.Descricao,
                ExtensaoArquivo = command.ExtensaoArquivo,
                Arquivo = command.Arquivo, // Arquivo em byte[]
                URLMiniatura = command.URLMiniatura,
                Ativo = command.Ativo,
                DataCadastro = command.DataCadastro == default ? DateTime.UtcNow : command.DataCadastro,
                CodigoGaleria = command.CodigoGaleria,
                CodigoUsuario = command.CodigoUsuario
            };

            // Salve o item e retorne
            return await _itemRepository.CriarItemAsync(itemGaleria);
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

        public async Task AlterarItemAsync(ItemGaleria itemGaleria, int codigoUsuario)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(codigoUsuario);
            if (usuario == null || usuario.Perfil != "Suporte")
                throw new UnauthorizedAccessException("Apenas usuários com perfil de suporte podem gerenciar a galeria.");

            byte[] arquivo = itemGaleria.Arquivo;


            if (itemGaleria.CodigoItemGaleria != 0)
            {
                await _itemRepository.AtualizarItemAsync(itemGaleria);   
            }
        }
        public async Task <ItemGaleria>ExcluirItemAsync(ExcluirItemGaleriaCommand command)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(command.CodigoUsuario);
            if (usuario == null || usuario.Perfil != "Suporte")
                throw new UnauthorizedAccessException("Apenas usuários com perfil de suporte podem gerenciar a galeria.");


            var item = await _itemRepository.ObterPorIdAsync(command.CodigoItemGaleria);
            if (item == null)
                throw new InvalidOperationException("Item não encontrado.");

            await _itemRepository.DeletarItemAsync(item);
            return item;
        }
    }
}
