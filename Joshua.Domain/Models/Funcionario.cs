namespace Joshua.Domain.Models
{
    public class Funcionario : Pessoa
    {
        public virtual IList<Endereco> Enderecos { get; set; }
    }
}
