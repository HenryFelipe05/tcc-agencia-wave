using Wave.Domain.Commands;
using Wave.Domain.Queries;

namespace Wave.Application.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa);
        Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync();
        Task<PessoaQuery> AdicionarPessoaAsync(PessoaCommand pessoaCommand);
        Task<bool> AlterarPessoaAsync(PessoaCommand pessoaCommand, int codigoPessoa);
        Task<bool> DeletarPessoaAsync(int codigoPessoa);
        Task<int?> RecuperarUltimoCodigoPessoaAsync();
    }
}
