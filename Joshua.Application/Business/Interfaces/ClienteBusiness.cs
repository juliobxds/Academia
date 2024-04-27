using AutoMapper;
using Joshua.Domain.Models;
using Joshua.Domain.ViewModels;
using Joshua.Infra.Data.Data;
using Joshua.Infra.Utils.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Joshua.Application.Business.Interfaces
{
    public class ClienteBusiness : IClienteBusiness
    {

        private readonly JoshuaContext db;
        private readonly IMapper mapper;
        public ClienteBusiness(JoshuaContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Response<ClienteViewModel>> ObterPorId(int id)
        {
            var response = new Response<ClienteViewModel>();

            try
            {
                if (id < 1)

                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Cliente não existe!";
                    return response;
                }

                var clienteDb = db.Clientes.Include(e => e.Enderecos).FirstOrDefault(c => c.Id == id);

                if (clienteDb == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Cliente não existe no banco de dados!";
                }

                clienteDb.CriadoEm = DateTime.Now;
                clienteDb.ModificadoEm = DateTime.Now;

                var clienteViewModel = mapper.Map<ClienteViewModel>(clienteDb);
                response.Entity = clienteViewModel;

                return response;

            } catch (Exception e)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao obter cliente! : " + e.Message;
            }

            return response;
        }
        public async Task<Response<List<ClienteViewModel>>> ListarTodos()
        {
            var response = new Response<List<ClienteViewModel>>();

            try
            {
                var clienteListaDB = db.Clientes.Include(e => e.Enderecos).AsNoTracking().ToList();

                var clientesViewModel = mapper.Map<List<ClienteViewModel>>(clienteListaDB);

                response.Entity = clientesViewModel;

                return response;

            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao obter todos os funcionarios : " + e.Message;
            }
            return response;

        }

        public async Task<Response<ClienteViewModel>> Adicionar(ClienteViewModel clienteVM)
        {
            var response = new Response<ClienteViewModel>();

            try
            {
                if (clienteVM == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Informaçoes de Cliente necessitam ser preenchidas!";
                    return response;
                }

                ValidarCliente(response, clienteVM);

                var clienteDB = db.Clientes.Include(e => e.Enderecos).AsNoTracking().FirstOrDefault(c => c.Nome.ToLower().Trim() == clienteVM.Nome.ToLower().Trim());

                if (clienteDB != null)
                {
                    response.Status = HttpStatusCode.NotAcceptable;
                    response.Message = "Esse Cliente já foi adicionado no banco de dados!";
                    return response;
                }

                foreach (var endereco in clienteVM.Enderecos)
                {
                    endereco.IdFuncionario = null; // foreach para setar que o IdFuncionario tem que ser igual a nulo qnd adicionar o cliente!!
                }

                clienteVM.CriadoEm = DateTime.Now;
                clienteVM.ModificadoEm = DateTime.Now;

                var clienteModel = mapper.Map<Cliente>(clienteVM);

                db.Clientes.Add(clienteModel);
                await db.SaveChangesAsync();

                response.Status = HttpStatusCode.OK;
                response.Message = "Cliente criado com Sucesso!";

                response.Entity = clienteVM;
                
                return response;

            } catch (Exception e)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao adicionar funcionario, usuário já existe! : " + e.Message; ;
            }
            return response;
        }

        public async Task<Response<ClienteViewModel>> Atualizar(int id, ClienteViewModel clienteVM)
        {
            var response = new Response<ClienteViewModel>();

            try
            {
                var clienteDB = db.Clientes.Include(e => e.Enderecos).AsNoTracking().FirstOrDefault(e => e.Id == id);

                if (clienteDB == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Cliente não existe no banco de dados!";
                    return response;
                }

                var enderecoViewModel = mapper.Map<List<Endereco>>(clienteVM.Enderecos);

                clienteDB.Nome = clienteVM.Nome;
                clienteDB.Email = clienteVM.Email;
                clienteDB.Celular = clienteVM.Celular;
                clienteDB.Enderecos = enderecoViewModel;
                clienteDB.CriadoEm = clienteVM.CriadoEm;
                clienteDB.ModificadoEm = clienteVM.ModificadoEm;

                db.Clientes.Update(clienteDB);
                await db.SaveChangesAsync();

                response.Status = HttpStatusCode.OK;
                response.Message = "Este Cliente foi atualizado com sucesso!";

                var clienteViewModel = mapper.Map<ClienteViewModel>(clienteDB);

                response.Entity = clienteViewModel;

                return response;


            } catch (Exception e)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao atualizar o Funcionário : " + e.Message; ;
            }
            return response;
        }

        public async Task<Response<ClienteViewModel>> Remover(int id)
        {
            var response = new Response<ClienteViewModel>();

            try
            {
                var clienteDB = db.Clientes.Include("Enderecos").AsNoTracking().FirstOrDefault(x => x.Id == id);

                if (clienteDB == null)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Message = "Cliente não existe no banco de dados!";
                    return response;
                }

                db.Clientes.Remove(clienteDB);
                await db.SaveChangesAsync();

                response.Status = HttpStatusCode.OK;
                response.Message = "Client removido com sucesso!";

                var clienteViewModel = mapper.Map<ClienteViewModel>(clienteDB);
                response.Entity = clienteViewModel;
                return response;

            } catch (Exception e)
            {

                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao remover funcionario : " + e.Message; ;
            }
            return response;
        }

        private void ValidarCliente(Response<ClienteViewModel> response, ClienteViewModel cliente)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(cliente.Nome))
            {
                messages.Add("O nome do usuário precisa ser preenchido!");
            }

            if (string.IsNullOrEmpty(cliente.Celular))
            {
                messages.Add("O celular do usuário precisa ser preenchido!");
            }

            if (string.IsNullOrEmpty(cliente.Email))
            {
                messages.Add("O email do usuário precisa ser preenchido!");
            }

            if (messages.Any())
            {
                response.Status = HttpStatusCode.NotAcceptable;
                response.Messages = messages.ToArray();
            }
        }

    }
}


 

