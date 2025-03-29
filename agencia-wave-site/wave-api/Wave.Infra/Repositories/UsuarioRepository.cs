using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
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

        public UsuarioRepository(WaveDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioQuery> AdicionarUsuarioAsync(UsuarioCommand usuarioCommand)
        {
            #region [ SQL ]
            var sql = new StringBuilder();

            sql.AppendLine("INSERT INTO Usuario (NomeUsuario,  ");
            sql.AppendLine("                     Email, ");
            sql.AppendLine("                     Telefone, ");
            sql.AppendLine("                     Senha, ");
            sql.AppendLine("                     CodigoPerfil, ");
            sql.AppendLine("                     CodigoPessoa, ");
            sql.AppendLine("	  VALUES (@NomeUsuario, ");
            sql.AppendLine("              @Email, ");
            sql.AppendLine("              @Telefone, ");
            sql.AppendLine("              @Senha, ");
            sql.AppendLine("              @CodigoPerfil, ");
            sql.AppendLine("              @CodigoPessoa) ");
            #endregion

            using (var connection = _context.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var linhasAfetadas = await connection.ExecuteAsync(sql.ToString(), new
                {
                    NomeUsuario = usuarioCommand.NomeUsuario,
                    Email = usuarioCommand.Email,
                    Telefone = usuarioCommand.Telefone,
                    Senha = usuarioCommand.Senha,
                    CodigoPerfil = usuarioCommand.CodigoPerfil,
                    CodigoPessoa = usuarioCommand.CodigoPessoa
                });

                if(linhasAfetadas > 0)
                {
                    var usuarioCriado = Usuario.MapearDadosUsuario(usuarioCommand);
                    return usuarioCriado;
                }
                else return null;
            }
        }

        public Task AtualizarUsuarioAsync(UsuarioCommand usuarioCommand, int codigoUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioQuery>> RecuperarTodosUsuariosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
