using Wave.Application.Command;
using Wave.Application.Queries;

namespace Wave.Application.Interfaces.Repository
{
    public interface IPessoaRepository
    {
        Task<PessoaQuery> RecuperarPessoa(int codigoPessoa);
        Task<IEnumerable<PessoaQuery>> RecuperarListaPessoas();
        Task<PessoaQuery> AdicionarPessoa(PessoaCommand pessoaCommand);
        Task<PessoaQuery> AlterarPessoa(PessoaCommand pessoaCommand);
        Task<bool> DeletarPessoa(int codigoPessoa);
    }
}
