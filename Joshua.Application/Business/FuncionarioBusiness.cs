using AutoMapper;
using Joshua.Application.Business.Interfaces;
using Joshua.Domain.Models;
using Joshua.Domain.ViewModels;
using Joshua.Infra.Data.Data;
using Joshua.Infra.Utils.Transports;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Joshua.Application.Business
{

    public class FuncionarioBusiness : IFuncionarioBusiness
    {
        private readonly JoshuaContext db;
        private readonly IMapper mapper;

        public FuncionarioBusiness(JoshuaContext db, IMapper map)
        {
            this.db = db;
            mapper = map;
        }
        public async Task<Response<List<FuncionarioViewModel>>> ListarTodos()
        {
            var response = new Response<List<FuncionarioViewModel>>();

            try
            {
                var funcionarioListaDB = db.Funcionarios.Include(e => e.Enderecos).AsNoTracking().ToList();
                var funcionariosViewModel = mapper.Map<List<FuncionarioViewModel>>(funcionarioListaDB);
                response.Entity = funcionariosViewModel;

                return response;

            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao obter todos os funcionarios : " + e.Message;
            }
            return response;

        }
        public async Task<Response<FuncionarioViewModel>> ObterPorId(int id)
        {
            var response = new Response<FuncionarioViewModel>();

            try
            {
                //validar id
                if (id < 1)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Funcionario não pode ser encontrado!";
                    return response;
                }

                //TODO: Validações 
                var funcionarioDb = db.Funcionarios.Include(e => e.Enderecos).FirstOrDefault(f => f.Id == id);

                if (funcionarioDb == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Funcionario não pode ser encontrado";
                    return response;
                }

                funcionarioDb.CriadoEm = DateTime.Now;
                funcionarioDb.ModificadoEm = DateTime.Now;


                response.Status = HttpStatusCode.OK;
                response.Message = "Funcionario encontrado!";

                var funcionarioViewModel = mapper.Map<FuncionarioViewModel>(funcionarioDb);
                response.Entity = funcionarioViewModel;

                return response;

            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao obter funcionario : " + e.Message;
            }

            return response;
        }

        public async Task<Response<FuncionarioViewModel>> Adicionar(FuncionarioViewModel funcionarioVM)
        {
            var response = new Response<FuncionarioViewModel>();
            try
            {
                if (funcionarioVM == null)
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Message = "As informações do funcionário precisam estar preenchidas!";

                    return response;
                }

                ValidarFuncionario(response, funcionarioVM);

                var funcionarioBD = db.Funcionarios.Include(e => e.Enderecos).AsNoTracking().FirstOrDefault(f => f.Nome.ToLower().Trim() == funcionarioVM.Nome.ToLower().Trim());

                if (funcionarioBD != null)
                {
                    response.Status = HttpStatusCode.NotAcceptable;
                    response.Message = "Esse Funcionário já foi cadastrado no banco de dados!";

                    return response;

                }
                funcionarioVM.CriadoEm = DateTime.Now;
                funcionarioVM.ModificadoEm = DateTime.Now;


                var funcionarioModel = mapper.Map<Funcionario>(funcionarioVM);

                db.Funcionarios.Add(funcionarioModel);
                await db.SaveChangesAsync();

                response.Entity = funcionarioVM;

            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao adicionar funcionario, usuário já existe! : " + e.Message;
            }

            return response;
        }

        public async Task<Response<FuncionarioViewModel>> Atualizar(int id, FuncionarioViewModel funcionarioVM)
        {
            var response = new Response<FuncionarioViewModel>();

            try
            {
                var funcionarioBd = db.Funcionarios.Include(e => e.Enderecos).AsNoTracking().FirstOrDefault(f => f.Id == id);

                if (funcionarioBd == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Funcionario não pode ser encontrado";

                    return response;

                }
                var enderecoViewModel = mapper.Map<List<Endereco>>(funcionarioVM.Enderecos); 

                funcionarioBd.Nome = funcionarioVM.Nome;
                funcionarioBd.Email = funcionarioVM.Email;
                funcionarioBd.Celular = funcionarioVM.Celular;
                funcionarioBd.Enderecos = enderecoViewModel;
                funcionarioBd.CriadoEm = funcionarioVM.CriadoEm;
                funcionarioBd.ModificadoEm = funcionarioVM.ModificadoEm;

                db.Funcionarios.Update(funcionarioBd);
                await db.SaveChangesAsync();

                response.Status = HttpStatusCode.OK;
                response.Message = "Este Funcionário foi atualizado!";

                var map = mapper.Map<FuncionarioViewModel>(funcionarioBd);

                response.Entity = map;
                return response;


            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao atualizar o Funcionário : " + e.Message;

            }
            return response;

        }
        public async Task<Response<FuncionarioViewModel>> Remover(int id)
        {
            var response = new Response<FuncionarioViewModel>();

            try
            {
                var FuncionarioDb = db.Funcionarios.AsNoTracking().Include("Enderecos").FirstOrDefault(f => f.Id == id);

                if (FuncionarioDb == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Funcionario não pode ser encontrado";

                    return response;
                }

                db.Funcionarios.Remove(FuncionarioDb);
                db.SaveChanges();

                response.Status = HttpStatusCode.OK;
                response.Message = "Funcionário excluido com sucesso!";

                var mapreverse = mapper.Map<FuncionarioViewModel>(FuncionarioDb);

                response.Entity = mapreverse;

                return response;

            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao remover funcionario : " + e.Message;

            }
            return response;
        }


        // método para validar as informaçoes de funcionario!!
        private void ValidarFuncionario(Response<FuncionarioViewModel> response, FuncionarioViewModel funcionario)
        {
            var messages = new List<string>();

            if (string.IsNullOrEmpty(funcionario.Nome))
            {
                messages.Add("O nome do usuário precisa ser preenchido!");
            }

            if (string.IsNullOrEmpty(funcionario.Celular))
            {
                messages.Add("O celular do usuário precisa ser preenchido!");
            }

            if (string.IsNullOrEmpty(funcionario.Email))
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