using Wave.Application.Services.Interfaces;
using Wave.Domain.Commands;
using Wave.Domain.Queries;
using Wave.Domain.Repository;

namespace Wave.Application.Services
{
    public class PessoaService : IPessoaService
	{
		private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<bool> AdicionarPessoaAsync(PessoaCommand pessoaCommand)
		{
			return await _pessoaRepository.AdicionarPessoaAsync(pessoaCommand);
		}

		public async Task<bool> AlterarPessoaAsync(PessoaCommand pessoaCommand, int codigoPessoa)
		{
			return await _pessoaRepository.AlterarPessoaAsync(pessoaCommand, codigoPessoa);
		}

		public async Task<bool> DeletarPessoaAsync(int codigoPessoa)
		{
			return await _pessoaRepository.DeletarPessoaAsync(codigoPessoa);
		}

		public async Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync()
		{
            return await _pessoaRepository.RecuperarListaPessoasAsync();
        }

		public async Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa)
		{
			return await _pessoaRepository.RecuperarPessoaAsync(codigoPessoa);
		}
	}
}
