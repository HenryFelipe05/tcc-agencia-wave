using Wave.Application.Commands;
using Wave.Application.Interfaces.Service;
using Wave.Application.Queries;

namespace Wave.Application.Services
{
	public class PessoaService : IPessoaService
	{
		public Task<PessoaQuery> AdicionarPessoaAsync(PessoaCommand pessoaCommand)
		{
			throw new NotImplementedException();
		}

		public Task<PessoaQuery> AlterarPessoaAsync(PessoaCommand pessoaCommand)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeletarPessoaAsync(int codigoPessoa)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync()
		{
			throw new NotImplementedException();
		}

		public Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa)
		{
			throw new NotImplementedException();
		}
	}
}
