using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joshua.Domain.Models
{
    public class Endereco : Base
    {
        public string Logradouro { get; set; }
        public string Rua { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int? IdFuncionario { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
