﻿namespace Joshua.Domain.Models
{
    public class Funcionario : Base
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        //IList<Endereco> enderecos { get; set; }

    }
}
