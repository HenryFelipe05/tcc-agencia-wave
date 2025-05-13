using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Usuario Usuario { get; set; }

        [ForeignKey("ItemGaleria")]
        public int CodigoItemGaleria { get; set; }
        [JsonIgnore]
        public ItemGaleria ItemGaleria { get; set; }
    }
}
