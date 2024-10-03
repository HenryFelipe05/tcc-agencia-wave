namespace Wave.Application.Commands
{
	public class PessoaCommand
	{
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public string Email { get; set; }
		public string Telefone { get; set; }
		public string Documento { get; set; }
		public DateTime DataNascimento { get; set; }
		public int CodigoTipoPessoa { get; set; }
		public int CodigoGenero { get; set; }

	}
}
