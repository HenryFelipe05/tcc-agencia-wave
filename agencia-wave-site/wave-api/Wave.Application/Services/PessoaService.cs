using Wave.Application.Commands;
using Wave.Application.Interfaces.Repository;
using Wave.Application.Interfaces.Service;
using Wave.Application.Queries;

namespace Wave.Application.Services
{
	public class PessoaService : IPessoaService
	{
		private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task AdicionarPessoaAsync(PessoaCommand pessoaCommand)
		{
			await _pessoaRepository.AdicionarPessoaAsync(pessoaCommand);
		}

		public async Task AlterarPessoaAsync(PessoaCommand pessoaCommand, int codigoPessoa)
		{
			await _pessoaRepository.AlterarPessoaAsync(pessoaCommand, codigoPessoa);
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
