using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wave.Application.Services.Interfaces;
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
        public async Task<IEnumerable<Assinatura>> ObterTodasAsync()
        {
            return await _assinaturaRepository.ObterTodasAsync();
        }
        public async Task<Assinatura> ObterPorIdAsync(int CodigoAssinatura)
        {
            return await _assinaturaRepository.ObterPorIdAsync(CodigoAssinatura);
        }
        public async Task<Assinatura> CriarAssinaturaAsync(Assinatura assinatura)
        {
            return await _assinaturaRepository.CriarAssinaturaAsync(assinatura);
        }

        public async Task<Assinatura> AtualizarAssinaturaAsync(Assinatura assinatura)
        {
            return await _assinaturaRepository.AtualizarAssinaturaAsync(assinatura);
        }

        public async Task<Assinatura> RemoverAssinaturaAsync(int CodigoAssinatura)
        {
            return await _assinaturaRepository.RemoverAssinaturaAsync(CodigoAssinatura);
        }
    }
}
