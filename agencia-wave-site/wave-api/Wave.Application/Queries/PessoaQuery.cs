namespace Wave.Application.Queries
{
    public class PessoaQuery
    {
        public int CodigoPessoa { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TipoPessoa { get; set; }
        public string Genero { get; set; }
    }
}
