using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("Genero")]
    public class Genero
    {
        [Key]
        public int CodigoGenero { get; set; }
        public string Descricao { get; set; }
    }
}
