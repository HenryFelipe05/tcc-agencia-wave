namespace Wave.Domain.Queries
{
    public class UsuarioQuery
    {
        public int CodigoUsuario { get; set; }
        public int CodigoPessoa { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
