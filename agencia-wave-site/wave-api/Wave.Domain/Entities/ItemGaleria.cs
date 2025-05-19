using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public byte[] Arquivo { get; set; }
        public string URLMiniatura { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        [ForeignKey("TipoAssinatura")]
        public int CodigoTipoGaleria { get; set; }
        public TipoAssinatura TipoAssinatura { get; set; }

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
