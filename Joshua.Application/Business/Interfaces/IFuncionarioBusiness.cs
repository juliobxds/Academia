using Joshua.Domain.Models;
using Joshua.Infra.Utils.Transports;

namespace Joshua.Application.Business.Interfaces
{
    public interface IFuncionarioBusiness
    {
        Response<Funcionario> GetById(int id);
    }
}
