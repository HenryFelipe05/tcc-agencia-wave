using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Enums;
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
            if (string.IsNullOrEmpty(command.Titulo) || string.IsNullOrEmpty(command.Descricao))
            {
                throw new ArgumentException("Título e descrição são obrigatórios.");
            }

            if (command.Arquivo == null || command.Arquivo.Length == 0)
            {
                throw new ArgumentException("Arquivo não pode ser nulo ou vazio.");
            }

            var extensao = Path.GetExtension(command.Arquivo.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(pastaUploads))
                Directory.CreateDirectory(pastaUploads);

            var caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await command.Arquivo.CopyToAsync(stream);
            }


            var itemGaleria = new ItemGaleria
            {
                Titulo = command.Titulo,
                Descricao = command.Descricao,
                ExtensaoArquivo = command.ExtensaoArquivo,
                Arquivo = nomeArquivo,
                URLMiniatura = command.URLMiniatura,
                Ativo = command.Ativo,
                DataCadastro = DateTime.UtcNow,
                CodigoGaleria = command.CodigoGaleria,
                CodigoUsuario = command.CodigoUsuario,
            };

            return await _itemRepository.CriarItemAsync(itemGaleria);
        }



        public async Task<string> BaixarItemAsync(ItemGaleriaCommand command)
        {
            var item = await _itemRepository.ObterPorIdAsync(command.CodigoItemGaleria);

            if (item == null)
                throw new InvalidOperationException("Item da galeria não encontrado.");

            if (string.IsNullOrEmpty(item.Arquivo))
                throw new InvalidOperationException("Arquivo não disponível para este item.");

            var pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var caminhoArquivo = Path.Combine(pastaUploads, item.Arquivo);

            if (!File.Exists(caminhoArquivo))
                throw new FileNotFoundException("Arquivo não encontrado no servidor.");

            return caminhoArquivo;
        }



        public async Task<IEnumerable<ItemGaleria>> BuscarItensAsync(ItemGaleriaQuery query)
        {
            var itens = await _itemRepository.ListarTodosAsync();

            if (itens is null || !itens.Any())
                throw new InvalidOperationException("Itens não encontrados");

            if (!string.IsNullOrEmpty(query.TipoArquivo))
                itens = itens.Where(i => i.ExtensaoArquivo.Equals(query.TipoArquivo, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(query.Pesquisa))
                itens = itens.Where(i => i.Titulo.Contains(query.Pesquisa, StringComparison.OrdinalIgnoreCase) ||
                                         i.Descricao.Contains(query.Pesquisa, StringComparison.OrdinalIgnoreCase));

            return itens.ToList();
        }

        public async Task AlterarItemAsync(AlteraItemGaleriaCommand command)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(command.CodigoUsuario);

            if (usuario == null || usuario.Perfil != PerfilEnum.Perfis.Suporte.ToString())
                throw new UnauthorizedAccessException("Apenas usuários com perfil de suporte podem gerenciar a galeria.");

            var item =  await _itemRepository.ObterPorIdAsync(command.CodigoItemGaleria) ?? throw new InvalidOperationException("Item não encontrado.");

            var extensao = Path.GetExtension(command.Arquivo.FileName);
            var pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var caminhoArquivo = Path.Combine(pastaUploads, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await command.Arquivo.CopyToAsync(stream);
            }

            item.Titulo = command.Titulo;
            item.Descricao = command.Descricao;
            item.ExtensaoArquivo = command.ExtensaoArquivo;
            item.Arquivo = nomeArquivo;
            item.URLMiniatura = command.URLMiniatura;
            item.Ativo = command.Ativo;

          await _itemRepository.AtualizarItemAsync(item);  
        }

        public async Task <ItemGaleria>ExcluirItemAsync(int codigoItemGaleria, int codigoUsuario)
        {
            var usuario = await _usuarioRepository.RecuperarUsuarioAsync(codigoUsuario);
            if(usuario == null || usuario.Perfil != PerfilEnum.Perfis.Suporte.ToString())
                throw new UnauthorizedAccessException("Apenas usuários com perfil de suporte podem gerenciar a galeria.");

            return await _itemRepository.DeletarItemAsync(codigoItemGaleria);
           
        }

        public async Task<ItemGaleriaCommand> ObterItemAsync(int codigoItemGaleria)
        {
            var item = await _itemRepository.ObterPorIdAsync(codigoItemGaleria);

            if (item == null)
                throw new UnauthorizedAccessException("Item não encontrado");

            return new ItemGaleriaCommand
            {
                CodigoItemGaleria = item.CodigoItemGaleria,
                Titulo = item.Titulo,
                Descricao = item.Descricao,
                ExtensaoArquivo = item.ExtensaoArquivo,
                Arquivo = item.Arquivo,
                URLMiniatura = item.URLMiniatura,
                Ativo = item.Ativo,
                DataCadastro = item.DataCadastro,
                CodigoGaleria = item.CodigoGaleria,
                CodigoUsuario = item.CodigoUsuario
            };
        }
    }
}
