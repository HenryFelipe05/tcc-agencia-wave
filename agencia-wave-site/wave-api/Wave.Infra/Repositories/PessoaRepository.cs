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

            sql.AppendLine("INSERT INTO Pessoa(Nome, ");
            sql.AppendLine("                   Sobrenome, ");
            sql.AppendLine("                   DataNascimento, ");
            sql.AppendLine("                   CodigoGenero) "); 
            sql.AppendLine("      VALUES (@Nome, ");
            sql.AppendLine("              @Sobrenome, ");
            sql.AppendLine("              @DataNascimento, ");
            sql.AppendLine("              @CodigoGenero)");
            #endregion

            using (var connection = _context.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var linhasAfetadas = await connection.ExecuteAsync(sql.ToString(), new
                {
                    Nome = pessoaCommand.Nome,
                    Sobrenome = pessoaCommand.Sobrenome,
                    DataNascimento = pessoaCommand.DataNascimento,
                    CodigoGenero = pessoaCommand.CodigoGenero,
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
                    DataNascimento = pessoaCommand.DataNascimento,
                    CodigoGenero = pessoaCommand.CodigoGenero,
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
            sql.AppendLine("		p.DataNascimento, ");
            sql.AppendLine("		g.Descricao AS Genero ");
            sql.AppendLine("   FROM Pessoa p ");
            sql.AppendLine("  INNER JOIN Genero g ON(g.CodigoGenero = p.CodigoGenero) ");
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
            sql.AppendLine("		p.DataNascimento, ");
            sql.AppendLine("		g.Descricao AS Genero ");
            sql.AppendLine("   FROM Pessoa p ");
            sql.AppendLine("  INNER JOIN Genero g ON(g.CodigoGenero = p.CodigoGenero) ");
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
