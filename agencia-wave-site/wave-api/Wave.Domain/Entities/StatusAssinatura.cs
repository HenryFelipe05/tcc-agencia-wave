using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("StatusAssinatura")]
    public class StatusAssinatura
    {
        [Key] 
        public int CodigoStatusAssinatura { get; set; }
        public string Descricao { get; set; }
    }
}
