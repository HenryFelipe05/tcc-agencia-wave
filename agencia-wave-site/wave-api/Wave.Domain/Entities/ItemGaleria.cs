using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Wave.Domain.Enums;

namespace Wave.Domain.Entities
{
    [Table("ItemGaleria")]
    public class ItemGaleria
    {
        [Key]
        public int CodigoItemGaleria { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string Arquivo { get; set; }
        public string URLMiniatura { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Exclusivo { get; set; }

        [ForeignKey("Galeria")]
        public int CodigoGaleria { get; set; }
        [JsonIgnore]
        public Galeria Galeria { get; set; }

        
        [ForeignKey("Usuario")]
        public int CodigoUsuario { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }
    }
}
