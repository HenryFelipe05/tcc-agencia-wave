using System;
using System.Text.Json.Serialization;

namespace Wave.Domain.Commands
{
    public class ExcluirItemGaleriaCommand
    {
        public int CodigoItemGaleria { get; set; }
        public int CodigoUsuario { get; set; }
    }
}
