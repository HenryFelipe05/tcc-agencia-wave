using Dapper;
using System.Data;
using System.Text;
using Wave.Domain.Commands;
using Wave.Domain.Queries;
using Wave.Domain.Repository;
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

        public async Task<bool> AdicionarPessoaAsync(PessoaCommand pessoaCommand)
        {
            #region [ SQL ]
            var sql = new StringBuilder();

            sql.AppendLine(" INSERT INTO Pessoa(Nome, ");
            sql.AppendLine("					Sobrenome, ");
            sql.AppendLine("					Documento, ");
            sql.AppendLine("					DataNascimento, ");
            sql.AppendLine("					CodigoGenero, ");
            sql.AppendLine("					CodigoTipoPessoa) ");
            sql.AppendLine("	  VALUES (@Nome, ");
            sql.AppendLine("			  @Sobrenome, ");
            sql.AppendLine("			  @Documento, ");
            sql.AppendLine("			  @DataNascimento, ");
            sql.AppendLine("			  @CodigoGenero, ");
            sql.AppendLine("			  @CodigoTipoPessoa) ");
            #endregion

            using (var connection = _context.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                
                var linhasAfetadas = await connection.ExecuteAsync(sql.ToString(), new
                {
                    Nome = pessoaCommand.Nome,
                    Sobrenome = pessoaCommand.Sobrenome,
                    Documento = pessoaCommand.Documento,
                    DataNascimento = pessoaCommand.DataNascimento,
                    CodigoGenero = pessoaCommand.CodigoGenero,
                    CodigoTipoPessoa = pessoaCommand.CodigoTipoPessoa
                });

				return linhasAfetadas > 0;
			}
        }

        public async Task<bool> AlterarPessoaAsync(PessoaCommand pessoaCommand, int codigoPessoa)
        {
            #region [ SQL ]
            var sql = new StringBuilder();

            sql.AppendLine(" UPDATE Pessoa ");
            sql.AppendLine("    SET Nome = @Nome, ");
            sql.AppendLine("        Sobrenome = @Sobrenome, ");
            sql.AppendLine("        Email = @Email, ");
            sql.AppendLine("        Telefone = @Telefone, ");
            sql.AppendLine("        Documento = @Documento, ");
            sql.AppendLine("        DataNascimento = @DataNascimento, ");
            sql.AppendLine("        CodigoTipoPessoa = @CodigoTipoPessoa ");
            sql.AppendLine("  WHERE CodigoPessoa = @CodigoPessoa ");
            #endregion

            using (var connection = _context.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                
				var linhasAfetadas = await connection.ExecuteAsync(sql.ToString(), new
                {
                    CodigoPessoa = codigoPessoa,
                    Nome = pessoaCommand.Nome,
                    Sobrenome = pessoaCommand.Sobrenome,
                    Documento = pessoaCommand.Documento,
                    DataNascimento = pessoaCommand.DataNascimento,
                    CodigoGenero = pessoaCommand.CodigoGenero,
                    CodigoTipoPessoa = pessoaCommand.CodigoTipoPessoa
                });

				return linhasAfetadas > 0;
			}
        }

        public async Task<bool> DeletarPessoaAsync(int codigoPessoa)
        {
            #region [ SQL ]
            var sql = new StringBuilder();

            sql.AppendLine(" DELETE FROM Pessoa ");
            sql.AppendLine("       WHERE CodigoPessoa = @CodigoPessoa ");
            #endregion

            using (var connection = _context.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                
                var linhasAfetadas = await connection.ExecuteAsync(sql.ToString(), new
                {
                    CodigoPessoa = codigoPessoa,
                });

                return linhasAfetadas > 0;
            }
        }

        public async Task<IEnumerable<PessoaQuery>> RecuperarListaPessoasAsync()
		{
            #region [ SQL ]
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
            #endregion

            using (var connection = _context.GetDbConnection())
			{
				if (connection.State == ConnectionState.Closed)
					connection.Open();

				return await connection.QueryAsync<PessoaQuery>(sql.ToString());
			}
		}

		public async Task<PessoaQuery> RecuperarPessoaAsync(int codigoPessoa)
		{
            #region [ SQL ]
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
            #endregion
			
            using (var connection = _context.GetDbConnection())
			{
				if (connection.State == ConnectionState.Closed)
					connection.Open();

				return await connection.QueryFirstOrDefaultAsync<PessoaQuery>(sql.ToString(), new { CodigoPessoa = codigoPessoa });
			}
		}
	}
}
