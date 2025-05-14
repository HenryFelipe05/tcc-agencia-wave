using Wave.Application.Services.Interfaces;
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
        private readonly IUsuarioRepository _usuarioRepository;

        public GaleriaService(IItemGaleriaRepository itemRepository, IUsuarioRepository usuarioRepository)
        {
            _itemRepository = itemRepository;
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
            var itens = await _itemRepository.ListarTodosAsync();

            if (itens is null || !itens.Any())
                throw new InvalidOperationException("Itens não encontrados");

            // Aplicar filtros opcionais
            if (!string.IsNullOrEmpty(query.TipoArquivo))
                itens = itens.Where(i => i.ExtensaoArquivo.Equals(query.TipoArquivo, StringComparison.OrdinalIgnoreCase));

            if (query.Exclusivo.HasValue && query.Exclusivo.Value)
                itens = itens.Where(i => i.CodigoUsuario != 0); // ou alguma lógica que defina o que é "exclusivo"

            if (!string.IsNullOrWhiteSpace(query.Pesquisa))
                itens = itens.Where(i => i.Titulo.Contains(query.Pesquisa, StringComparison.OrdinalIgnoreCase) ||
                                         i.Descricao.Contains(query.Pesquisa, StringComparison.OrdinalIgnoreCase));

            return itens.ToList();
        }

        public async Task <ItemGaleria>AlterarItemAsync(ItemGaleriaCommand itemGaleriaCommand)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(itemGaleriaCommand.CodigoUsuario);
            if (usuario == null || usuario.Perfil != "Suporte")
                throw new UnauthorizedAccessException("Apenas usuários com perfil de suporte podem gerenciar a galeria.");

            if (itemGaleriaCommand.CodigoItemGaleria == 0)
                throw new ArgumentException("Código do item inválido.");

            var itemExistente = await _itemRepository.ObterPorIdAsync(itemGaleriaCommand.CodigoItemGaleria);
            if (itemExistente == null)
                throw new InvalidOperationException("Item não encontrado.");

            // Atualiza os campos
            itemExistente.Titulo = itemGaleriaCommand.Titulo;
            itemExistente.Descricao = itemGaleriaCommand.Descricao;
            itemExistente.ExtensaoArquivo = itemGaleriaCommand.ExtensaoArquivo;
            itemExistente.Arquivo = itemGaleriaCommand.Arquivo;
            itemExistente.URLMiniatura = itemGaleriaCommand.URLMiniatura;
            itemExistente.Ativo = itemGaleriaCommand.Ativo;
            itemExistente.CodigoGaleria = itemGaleriaCommand.CodigoGaleria;
            itemExistente.CodigoUsuario = itemGaleriaCommand.CodigoUsuario;

            await _itemRepository.AtualizarItemAsync(itemExistente);

            return itemExistente;
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
