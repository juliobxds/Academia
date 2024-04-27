using Joshua.Domain.ViewModels;
using Joshua.Infra.Utils.Transports;

namespace Joshua.Application.Business.Interfaces
{
    public interface IClienteBusiness
    {
        Task<Response<ClienteViewModel>> ObterPorId(int id);
        Task<Response<List<ClienteViewModel>>> ListarTodos();
        Task<Response<ClienteViewModel>> Adicionar(ClienteViewModel clienteVM);
        Task<Response<ClienteViewModel>> Atualizar(int id, ClienteViewModel clienteVM);
        Task<Response<ClienteViewModel>> Remover(int id);
    }
}
