namespace Wave.Domain.Queries
{
    public class ItemGaleriaQuery
    {
        public string? TipoArquivo { get; set; }
        public string? TipoItem { get; set; }
        public string? Pesquisa { get; set; }


        public bool? GetFiltroExclusivo()
        {
            return TipoItem?.ToLower() switch
            {
                "exclusivo" => true,
                "gratuito" => false,
                _ => null
            };
        }
    }
}
