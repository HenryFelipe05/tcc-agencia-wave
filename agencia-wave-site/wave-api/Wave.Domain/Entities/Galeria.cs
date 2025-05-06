namespace Wave.Domain.Entities
{
    public class Galeria
    {
        public int CodigoGaleria { get; set; }
        public string Descricao { get; set; }

        public ICollection<ItemGaleria> ItemGalerias { get; set; }
    }
}
