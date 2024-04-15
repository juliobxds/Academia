namespace Joshua.Domain.Models
{
    public class Pessoa : CrudBase 
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
    }
}
