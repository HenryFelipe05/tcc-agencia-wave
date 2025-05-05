using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("Favorito")]
    public class Favorito
    {
        [Key]
        public int CodigoFavorito { get; set; }
        public DateTime DataFavorito { get; set; }

        [ForeignKey("Usuario")]
        public int CodigoUsuario { get; set; }

        [ForeignKey("ItemGaleria")]
        public int CodigoItemGaleria { get; set; }
    }
}
