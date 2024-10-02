using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wave.Domain.Entities
{
    [Table("Pefil")]
    public class Perfil
    {
        [Key]
        public int CodigoPerfil { get; set; }
        public string Descricao { get; set; }
    }
}
