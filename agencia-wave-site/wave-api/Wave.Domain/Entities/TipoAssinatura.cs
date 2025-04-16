using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("TipoAssinatura")]
    public class TipoAssinatura
    {
        [Key]
        public int CodigoTipoAssinatura { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
