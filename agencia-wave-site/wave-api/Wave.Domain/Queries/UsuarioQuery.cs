using Wave.Domain.Entities;

namespace Wave.Domain.Queries
{
    public class UsuarioQuery
    {
        public int CodigoUsuario { get; set; }
        public int CodigoPessoa { get; set; }
        public int CodigoGenero { get; set; }
        public string NomeUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
