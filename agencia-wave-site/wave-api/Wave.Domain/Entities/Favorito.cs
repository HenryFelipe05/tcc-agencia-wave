using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    public class Favorito
    {
        public int CodigoFavorito { get; set; }
        public DateTime DataFavorito { get; set; }

        [ForeignKey("Usuario")]
        public int CodigoUsuario { get; set; }

        [ForeignKey("ItemGaleria")]
        public int CodigoItemGaleria { get; set; }
    }
}
