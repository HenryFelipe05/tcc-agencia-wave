namespace Wave.Domain.Commands
{
    public class PessoaCommand
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Documento { get; set; }
        public int CodigoGenero { get; set; }
        public int CodigoTipoPessoa { get; set; }
    }
}
