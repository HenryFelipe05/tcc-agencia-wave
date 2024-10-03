using Dapper;
using System.Data;
using System.Text;
using Wave.Application.Commands;
using Wave.Application.Interfaces.Repository;
using Wave.Application.Queries;
using Wave.Domain.Entities;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
	public class PessoaRepository : IPessoaRepository
	{
		private readonly WaveDbContext _context;

		public PessoaRepository(WaveDbContext context)
		{
			_context = context;
		}

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

		public async Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync()
		{
			var sql = new StringBuilder();

			sql.AppendLine(" SELECT p.CodigoPessoa,  ");
			sql.AppendLine("		p.Nome, ");
			sql.AppendLine("		p.Sobrenome, ");
			sql.AppendLine("		p.Email, ");
			sql.AppendLine("		p.Telefone, ");
			sql.AppendLine("		p.Documento, ");
			sql.AppendLine("		p.DataNascimento, ");
			sql.AppendLine("		tp.Descricao AS TipoPessoa, ");
			sql.AppendLine("		g.Descricao AS Genero ");
			sql.AppendLine("   FROM Pessoa p ");
			sql.AppendLine("  INNER JOIN Genero g ON(g.CodigoGenero = p.CodigoGenero) ");
			sql.AppendLine("  INNER JOIN TipoPessoa tp ON(tp.CodigoTipoPessoa = p.CodigoTipoPessoa) ");
			sql.AppendLine("  ORDER BY p.Nome ");

			using (var connection = _context.GetDbConnection())
			{
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}

				return await connection.QueryAsync<PessoaQuery>(sql.ToString());
			}
		}

		public async Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa)
		{
			var sql = new StringBuilder();

			sql.AppendLine(" SELECT p.CodigoPessoa,  ");
			sql.AppendLine("		p.Nome, ");
			sql.AppendLine("		p.Sobrenome, ");
			sql.AppendLine("		p.Email, ");
			sql.AppendLine("		p.Telefone, ");
			sql.AppendLine("		p.Documento, ");
			sql.AppendLine("		p.DataNascimento, ");
			sql.AppendLine("		tp.Descricao AS TipoPessoa, ");
			sql.AppendLine("		g.Descricao AS Genero ");
			sql.AppendLine("   FROM Pessoa p ");
			sql.AppendLine("  INNER JOIN Genero g ON(g.CodigoGenero = p.CodigoGenero) ");
			sql.AppendLine("  INNER JOIN TipoPessoa tp ON(tp.CodigoTipoPessoa = p.CodigoTipoPessoa) ");
			sql.AppendLine("  WHERE p.CodigoPessoa = @CodigoPessoa ");

			using (var connection = _context.GetDbConnection())
			{
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}

				return await connection.QueryFirstOrDefaultAsync<PessoaQuery>(sql.ToString(), new { CodigoPessoa = codigoPessoa });
			}
		}
	}
}
