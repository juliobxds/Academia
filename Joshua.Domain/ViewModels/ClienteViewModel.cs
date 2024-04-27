using Joshua.Domain.Models;

namespace Joshua.Domain.ViewModels
{
    public class ClienteViewModel : Pessoa
    {
        public virtual IList<EnderecoViewModel> Enderecos { get; set; }
    }
}
