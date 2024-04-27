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
   
    public class ClienteController : ControllerBase
    {
        private readonly IClienteBusiness _cliente;

        public ClienteController(IClienteBusiness cliente)
        {
            _cliente = cliente;
        }

        [HttpGet("ObterPorId")]
        public async Task<Response<ClienteViewModel>> ObterPorId(int id)
        {
            return await _cliente.ObterPorId(id);
        }

        [HttpGet("ListarTodos")]
        public async Task<Response<List<ClienteViewModel>>> ListarTodos()
        {
            return await _cliente.ListarTodos();
        }

        [HttpPost("Adicionar")]
        public async Task<Response<ClienteViewModel>> Adicionar(ClienteViewModel clienteVM)
        {
            return await _cliente.Adicionar(clienteVM);
        }

        [HttpPut("Atualizar")]
        public async Task<Response<ClienteViewModel>> Atualizar(int id, ClienteViewModel clienteVM)
        {
            return await _cliente.Atualizar(id, clienteVM);
        }

        [HttpDelete("Deletar")]
        public async Task<Response<ClienteViewModel>> Remover(int id) 
        {
           return await _cliente.Remover(id);
        }
    }
}
