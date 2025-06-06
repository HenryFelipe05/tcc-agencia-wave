using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Queries;
using Wave.Domain.Repository;
using Wave.Infra.Data.Context;

namespace Wave.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly WaveDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioRepository(WaveDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UsuarioQuery> AdicionarUsuarioAsync(UsuarioCommand usuarioCommand)
        {
            #region [ SQL ]
            var sql = new StringBuilder();

            sql.AppendLine("INSERT INTO Usuario (NomeUsuario, ");
            sql.AppendLine("                     Email, ");
            sql.AppendLine("                     Telefone, ");
            sql.AppendLine("                     Senha, ");
            sql.AppendLine("                     CodigoPerfil, ");
            sql.AppendLine("                     CodigoPessoa, ");
            sql.AppendLine("                     Ativo)");
            sql.AppendLine("	  VALUES (@NomeUsuario, ");
            sql.AppendLine("              @Email, ");
            sql.AppendLine("              @Telefone, ");
            sql.AppendLine("              @Senha, ");
            sql.AppendLine("              @CodigoPerfil, ");
            sql.AppendLine("              @CodigoPessoa, ");
            sql.AppendLine("              @Ativo);");
            #endregion



            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var linhasAfetadas = await connection.ExecuteAsync(sql.ToString(), new
                {
                    NomeUsuario = usuarioCommand.NomeUsuario,
                    Email = usuarioCommand.Email,
                    Telefone = usuarioCommand.Telefone,
                    Senha = usuarioCommand.Senha,
                    CodigoPerfil = usuarioCommand.CodigoPerfil,
                    CodigoPessoa = usuarioCommand.CodigoPessoa,
                    Ativo = usuarioCommand.Ativo
                });

                if (linhasAfetadas > 0)
                {
                    var usuarioCriado = Usuario.MapearDadosUsuario(usuarioCommand);
                    return usuarioCriado;
                }
                else return null;
            }
        }

        public async Task AtualizarUsuarioAsync(UsuarioCommand usuarioCommand, int codigoUsuario)
        {
            var sql = new StringBuilder();

            #region[SQL]
            sql.AppendLine("UPDATE Usuario SET ");
            sql.AppendLine("    NomeUsuario = @NomeUsuario, ");
            sql.AppendLine("    Email = @Email, ");
            sql.AppendLine("    Telefone = @Telefone, ");
            sql.AppendLine("    Senha = @Senha, ");
            sql.AppendLine("    CodigoPerfil = @CodigoPerfil, ");
            sql.AppendLine("    Ativo = @Ativo ");
            sql.AppendLine("WHERE CodigoUsuario = @CodigoUsuario");
            #endregion

            using (var connection = _context.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                await connection.ExecuteAsync(sql.ToString(), new
                {
                    NomeUsuario = usuarioCommand.NomeUsuario,
                    Email = usuarioCommand.Email,
                    Telefone = usuarioCommand.Telefone,
                    Senha = usuarioCommand.Senha,
                    CodigoPerfil = usuarioCommand.CodigoPerfil,
                    Ativo = usuarioCommand.Ativo,
                    CodigoUsuario = codigoUsuario
                });
            }
        }

        public async Task<Usuario> BuscarUsuarioPorNomeOuEmailAsync(string identificador)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NomeUsuario == identificador || u.Email == identificador);
        }

        public async Task<IEnumerable<UsuarioQuery>> RecuperarTodosUsuariosAsync()
        {
            var sql = new StringBuilder();

            #region[SQL]
            sql.AppendLine("SELECT ");
            sql.AppendLine("    u.CodigoUsuario, ");
            sql.AppendLine("    u.NomeUsuario, ");
            sql.AppendLine("    u.Email, ");
            sql.AppendLine("    u.CodigoPerfil, ");
            sql.AppendLine("    u.Ativo ");
            sql.AppendLine("FROM Usuario u");
            sql.AppendLine("INNER JOIN Perfil p ON u.CodigoPerfil = p.CodigoPerfil ");
            sql.AppendLine("WHERE u.CodigoUsuario = @CodigoUsuario");
            #endregion

            using (var connection = _context.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var usuarios = await connection.QueryAsync<UsuarioQuery>(sql.ToString());
                return usuarios;
            }
        }

        public async Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario)
        {
            var sql = new StringBuilder();

            #region[SQL]

            sql.AppendLine("SELECT ");
            sql.AppendLine("    u.CodigoUsuario, ");
            sql.AppendLine("    u.NomeUsuario, ");
            sql.AppendLine("    u.Email, ");
            sql.AppendLine("    u.CodigoPerfil, ");
            sql.AppendLine("    p.Descricao AS Perfil, ");  // <- aqui traz o nome do perfil
            sql.AppendLine("    u.Ativo ");
            sql.AppendLine("FROM Usuario u ");
            sql.AppendLine("INNER JOIN Perfil p ON u.CodigoPerfil = p.CodigoPerfil ");
            sql.AppendLine("WHERE u.CodigoUsuario = @CodigoUsuario");
            #endregion

            using var connection = new SqlConnection(_context.Database.GetConnectionString());
            await connection.OpenAsync();

            return await connection.QueryFirstOrDefaultAsync<UsuarioQuery>(sql.ToString(), new { CodigoUsuario = codigoUsuario });

        }
    }
}