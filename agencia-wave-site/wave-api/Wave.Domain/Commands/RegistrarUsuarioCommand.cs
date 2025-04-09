﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wave.Domain.Commands
{
    public class RegistrarUsuarioCommand
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CodigoGenero { get; set; }
        public int CodigoPessoa { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirmada { get; set; }
        public bool Ativo { get; set; }
        public int CodigoPerfil { get; set; }
    }
}
