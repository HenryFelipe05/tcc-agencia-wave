using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wave.Domain.Commands
{
    public class AlteraItemGaleriaCommand
    {
        public int CodigoItemGaleria { get; set; }
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
