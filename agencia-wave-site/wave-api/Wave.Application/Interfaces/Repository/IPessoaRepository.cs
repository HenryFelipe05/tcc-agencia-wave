using Wave.Application.Commands;
using Wave.Application.Queries;

namespace Wave.Application.Interfaces.Repository
{
	public interface IPessoaRepository
	{
		Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa);
		Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync();
		Task<bool> AdicionarPessoaAsync(PessoaCommand pessoaCommand);
		Task<bool> AlterarPessoaAsync(PessoaCommand pessoaCommand, int codigoPessoa);
		Task<bool> DeletarPessoaAsync(int codigoPessoa);
	}
}
