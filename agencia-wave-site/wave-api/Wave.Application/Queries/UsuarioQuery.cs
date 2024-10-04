using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wave.Application.Queries
{
	public class UsuarioQuery
	{
        public int CodigoUsuario { get; set; }
        public int CodigoPessoa { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
