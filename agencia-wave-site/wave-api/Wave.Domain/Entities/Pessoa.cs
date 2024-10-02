using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public int CodigoPessoa { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }
        public DateTime DataNascimento { get; set; }

        [ForeignKey("TipoPessoa")]
        public int CodigoTipoPessoa { get; set; }

        [ForeignKey("Genero")]
        public int CodigoGenero { get; set; }
    }
}
