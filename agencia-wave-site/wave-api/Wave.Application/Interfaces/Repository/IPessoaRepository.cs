using Wave.Application.Commands;
using Wave.Application.Queries;

namespace Wave.Application.Interfaces.Repository
{
	public interface IPessoaRepository
	{
		Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa);
		Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync();
		Task<PessoaQuery> AdicionarPessoaAsync(PessoaCommand pessoaCommand);
		Task<PessoaQuery> AlterarPessoaAsync(PessoaCommand pessoaCommand);
		Task<bool> DeletarPessoaAsync(int codigoPessoa);
	}
}
