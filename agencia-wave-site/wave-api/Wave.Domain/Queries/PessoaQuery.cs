﻿namespace Wave.Domain.Queries
{
    public class PessoaQuery
    {
        public int CodigoPessoa { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
    }
}
