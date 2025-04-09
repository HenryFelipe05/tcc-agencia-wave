using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wave.Domain.Commands;

namespace Wave.Domain.Entities
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public int CodigoPessoa { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Documento { get; set; }
        public DateTime DataNascimento { get; set; }

        [ForeignKey("TipoPessoa")]
        public int CodigoTipoPessoa { get; set; }

        [ForeignKey("Genero")]
        public int CodigoGenero { get; set; }

        public static PessoaCommand MapearCommandPessoa(string Nome, string Sobrenome, DateTime dataNascimento, int codigoGenero)
        {
            return new PessoaCommand
            {
                CodigoGenero = codigoGenero,
                Nome = Nome,
                Sobrenome = Sobrenome,
                DataNascimento = dataNascimento,
            };
        }
    }
}
