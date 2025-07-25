﻿using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Wave.Domain.Commands
{
    public class ItemGaleriaCommand
    {
        public int CodigoItemGaleria { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string Arquivo { get; set; }
        public string URLMiniatura { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CodigoGaleria { get; set; }
        public int CodigoUsuario { get; set; }
    }
}
