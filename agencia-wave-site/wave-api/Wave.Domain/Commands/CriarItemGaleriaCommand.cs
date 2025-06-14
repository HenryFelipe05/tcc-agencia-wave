using Microsoft.AspNetCore.Http;

namespace Wave.Domain.Commands
{
    public class CriarItemGaleriaCommand
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ExtensaoArquivo { get; set; }
        public IFormFile Arquivo { get; set; }

        public string URLMiniatura { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CodigoGaleria { get; set; }
        public int CodigoUsuario { get; set; }
    }
}
