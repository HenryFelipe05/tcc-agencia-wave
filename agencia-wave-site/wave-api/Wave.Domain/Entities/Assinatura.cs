using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Wave.Domain.Entities
{
    [Table("Assinatura")]
    public class Assinatura
    {
        [Key]
        public Guid CodigoAssinatura { get; set; }
        public DateTime DataCadastro { get; set; }

        [ForeignKey("TipoAssinatura")]
        public int CodigoTipoAssinatura { get; set; }

        [ForeignKey("Usuario")]
        public int CodigoUsuario { get; set; }

        [ForeignKey("StatusAssinatura")]
        public int CodigoStatusAssinatura { get; set; }
    }
}
