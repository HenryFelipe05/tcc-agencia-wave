using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wave.Application.Commands;
using Wave.Application.Queries;

namespace Wave.Application.Interfaces.Repository
{
	public interface IUsuarioRepository
	{
		Task<UsuarioQuery> RecuperarUsuarioAsync(int codigoUsuario);
		Task<IEnumerable<UsuarioQuery>> RecuperarListaUsuariosAsync();
		Task<bool> AdicionarUsuarioAsync(UsuarioCommand usuarioCommand);
		Task<bool> InativarUsuarioAsync(int codigoUsuario);
		Task<bool> AlterarSenhaUsuarioAsync(int codigoUsuario, string senha);
		Task<bool> AlterarPerfilUsuarioAsync(int codigoUsuario, int codigoPerfil);
		Task<bool> DeletarUsuarioAsync(int codigoUsuario);
	}
}
