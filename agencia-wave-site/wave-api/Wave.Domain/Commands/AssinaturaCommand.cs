namespace Wave.Domain.Commands
{
    public class AssinaturaCommand
    {
        public Guid CodigoAssinatura { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public bool Ativa { get; set; }
        public int CodigoTipoAssinatura { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoStatusAssinatura { get; set; }
    }
}
