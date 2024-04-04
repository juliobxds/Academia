using Joshua.Domain.Models;
using Joshua.Domain.ViewModels;
using Joshua.Infra.Utils.Transports;

namespace Joshua.Application.Business.Interfaces
{
    public interface IFuncionarioBusiness
    {
        Task<Response<Funcionario>> ObterPorId(int id);
        Task<Response<List<Funcionario>>> ListarTodos();
        Task<Response<FuncionarioViewModel>> Adicionar(FuncionarioViewModel funcionarioVM);
        Task<Response<FuncionarioViewModel>> Atualizar(int id, FuncionarioViewModel funcionarioVM);
        Task<Response<FuncionarioViewModel>> Remover(int id);
    }
}
