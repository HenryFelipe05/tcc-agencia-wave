﻿using Wave.Application.Commands;
using Wave.Application.Queries;

namespace Wave.Application.Interfaces.Service
{
	public interface IPessoaService
	{
		Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa);
		Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync();
		Task AdicionarPessoaAsync(PessoaCommand pessoaCommand);
		Task AlterarPessoaAsync(PessoaCommand pessoaCommand, int codigoPessoa);
		Task<bool> DeletarPessoaAsync(int codigoPessoa);
	}
}
