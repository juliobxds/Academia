using Joshua.Application.Business.Interfaces;
using Joshua.Domain.ViewModels;
using Joshua.Infra.Utils.Transports;
using Microsoft.AspNetCore.Mvc;

namespace Joshua.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioBusiness _funcionario;

        public FuncionarioController(IFuncionarioBusiness funcionario)
        {
            _funcionario = funcionario;
        }

        [HttpGet("ObterPorId")]
        public async Task<Response<FuncionarioViewModel>> ObterPorId(int id)
        {
            return await _funcionario.ObterPorId(id);
        }

        [HttpGet("ListarTodos")]
        public async Task<Response<List<FuncionarioViewModel>>> ListarTodos()
        {
            return await _funcionario.ListarTodos();
        }

        [HttpPost("Adicionar")]
        public async Task<Response<FuncionarioViewModel>> Adicionar(FuncionarioViewModel funcionarioVM)
        {
            return await _funcionario.Adicionar(funcionarioVM);
        }

        [HttpPut("Atualizar")]
        public async Task<Response<FuncionarioViewModel>> Atualizar(int id, FuncionarioViewModel funcionarioVM)
        {
            return await _funcionario.Atualizar(id, funcionarioVM);
        }

        [HttpDelete("Deletar")]
        public async Task<Response<FuncionarioViewModel>> Remover(int id)
        {
            return await _funcionario.Remover(id);
        }

    }
}
