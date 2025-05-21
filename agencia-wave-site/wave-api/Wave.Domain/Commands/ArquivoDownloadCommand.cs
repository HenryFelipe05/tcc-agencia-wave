namespace Wave.Domain.Commands
{
    public class ArquivoDownloadCommand
    {
        public byte[] Conteudo { get; set; }
        public string NomeArquivo { get; set; }
        public string TipoMime { get; set; }
    }
}
