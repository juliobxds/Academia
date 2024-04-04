using Joshua.Application.Business;
using Joshua.Application.Business.Interfaces;
using Joshua.Domain.Models;
using Joshua.Domain.ViewModels;
using Joshua.Infra.Data.Data;
using Joshua.Infra.Utils.Transports;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
        public Task<Response<Funcionario>> ObterPorId(int id)
        {
            var funcionarioPorId = _funcionario.ObterPorId(id);

            return funcionarioPorId;
        }

        [HttpGet("ListarTodos")]
        public Task<Response<List<Funcionario>>> ListarTodos()
        {
            return _funcionario.ListarTodos();
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
        public Task<Response<FuncionarioViewModel>> Remover(int id) 
        {
           var delete = _funcionario.Remover(id);
            return delete;
        }

    }
}
