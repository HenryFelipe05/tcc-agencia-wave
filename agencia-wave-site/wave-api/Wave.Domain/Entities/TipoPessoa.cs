using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("TipoPessoa")]
    public class TipoPessoa
    {
        [Key]
        public int CodigoTipoPessoa { get; set; }
        public string Descricao { get; set; }
    }
}
