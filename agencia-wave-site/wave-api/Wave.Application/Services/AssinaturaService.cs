using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Repository;

namespace Wave.Application.Services
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly IAssinaturaRepository _assinaturaRepository;

        public AssinaturaService(IAssinaturaRepository assinaturaRepository)
        {
            _assinaturaRepository = assinaturaRepository;
        }

        public async Task<IEnumerable<Assinatura>> ObterPorUsuarioIdAsync(int codigoUsuario)
        {
           return  await _assinaturaRepository.ObterPorUsuarioIdAsync(codigoUsuario);               
        }

        public async Task<IEnumerable<Assinatura>> ObterTodasAsync()
        {
            return await _assinaturaRepository.ObterTodasAsync();
        }

        public async Task<Assinatura> ObterPorIdAsync(Guid codigoAssinatura)
        {
            return await _assinaturaRepository.ObterPorIdAsync(codigoAssinatura);
        }

        public async Task<Assinatura> CriarAssinaturaAsync(AssinaturaCommand assinaturaCommand)
        {
            var assinaturasDoUsuario = await _assinaturaRepository.ObterPorUsuarioIdAsync(assinaturaCommand.CodigoUsuario);

            var assinaturaAtiva = assinaturasDoUsuario.FirstOrDefault(a => a.Ativa);

            if(assinaturaAtiva != null)
            {
                //verifica se a assinatura esta ativa se sim cancela ela e depois cria uma nova
                assinaturaAtiva.Ativa = false;
                assinaturaAtiva.DataCancelamento = DateTime.UtcNow;

                await _assinaturaRepository.AtualizarAssinaturaAsync(assinaturaAtiva);
            }

            assinaturaCommand.Ativa = true;
            assinaturaCommand.DataCadastro = DateTime.UtcNow;

            var item = new Assinatura
            {
                DataCadastro = assinaturaCommand.DataCadastro,
                DataCancelamento = assinaturaCommand.DataCancelamento,
                Ativa = assinaturaCommand.Ativa,
                CodigoTipoAssinatura = assinaturaCommand.CodigoTipoAssinatura,
                CodigoUsuario = assinaturaCommand.CodigoUsuario,
                CodigoStatusAssinatura = assinaturaCommand.CodigoStatusAssinatura
            };

            return await _assinaturaRepository.CriarAssinaturaAsync(item);         
        }


        public async Task<Assinatura> AtualizarAssinaturaAsync(AssinaturaCommand assinaturaCommand)
        {
            var itemAtualizado = new Assinatura
            {
                CodigoAssinatura = assinaturaCommand.CodigoAssinatura,
                DataCadastro = assinaturaCommand.DataCadastro,
                DataCancelamento = assinaturaCommand.DataCancelamento,
                Ativa = assinaturaCommand.Ativa,
                CodigoTipoAssinatura = assinaturaCommand.CodigoTipoAssinatura,
                CodigoUsuario = assinaturaCommand.CodigoUsuario,
                CodigoStatusAssinatura = assinaturaCommand.CodigoStatusAssinatura
            };

            return await _assinaturaRepository.AtualizarAssinaturaAsync(itemAtualizado);
        }


        public async Task<Assinatura> RemoverAssinaturaAsync(Guid codigoAssinatura)
        {
            return await _assinaturaRepository.RemoverAssinaturaAsync(codigoAssinatura);
        }

        public async Task<IEnumerable<Assinatura>> ObterHistoricoDeAssinaturasAsync(int codigoUsuario)
        {
            var todasAssinaturas = await _assinaturaRepository.ObterPorUsuarioIdAsync(codigoUsuario);

            return todasAssinaturas.Where(a => !a.Ativa).OrderByDescending(a => a.DataCancelamento);
        }

        public async Task<string> CancelarAssinaturaAsync(Guid codigoAssinatura)
        {
            var assinatura = await _assinaturaRepository.ObterPorIdAsync(codigoAssinatura);

            if (assinatura == null)
                return await Task.FromResult("Assinatura não encontrada.");

            if (!assinatura.Ativa)
                return await Task.FromResult("A assinatura já está cancelada.");

            assinatura.Ativa = false;
            assinatura.DataCancelamento = DateTime.UtcNow;

            await _assinaturaRepository.AtualizarAssinaturaAsync(assinatura);

            return await Task.FromResult("Assinatura cancelada com sucesso.");
        }
    }
}
