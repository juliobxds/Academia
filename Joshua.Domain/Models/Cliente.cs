namespace Joshua.Domain.Models
{
    public class Cliente : Pessoa
    {
        public virtual IList<Endereco> Enderecos { get; set; }
    }
}
