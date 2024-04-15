using Joshua.Domain.Models;

namespace Joshua.Domain.ViewModels
{
    public class FuncionarioViewModel : Pessoa
    {
        public virtual IList<EnderecoViewModel> Enderecos { get; set; }
    }
}
