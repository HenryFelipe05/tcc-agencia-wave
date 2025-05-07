using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("Galeria")]
    public class Galeria
    {
        [Key]
        public int CodigoGaleria { get; set; }
        public string Descricao { get; set; }

        public ICollection<ItemGaleria> ItemGalerias { get; set; }
    }
}
