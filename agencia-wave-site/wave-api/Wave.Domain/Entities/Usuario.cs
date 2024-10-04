using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int CodigoUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        [ForeignKey("Pessoa")]
        public int CodigoPessoa { get; set; }

        [ForeignKey("Perfil")]
        public int CodigoPerfil { get; set; }
    }
}
