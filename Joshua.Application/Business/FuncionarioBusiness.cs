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
            this.mapper = map;
        }
        public async Task<Response<List<Funcionario>>> ListarTodos()
        {
            var response = new Response<List<Funcionario>>();

            try
            {
                response.Entity = db.Funcionarios.AsNoTracking().ToList();

                if (response == null)
                {
                  response.Status = HttpStatusCode.NotFound;
                } else
                  response.Status = HttpStatusCode.OK;
                {
                  await db.Funcionarios.ToListAsync();
                  await db.SaveChangesAsync();

                  return response;
                }
            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao obter todos os funcionarios : " + e.Message;
            }
            return response;
            
        }
        public async Task<Response<Funcionario>> ObterPorId(int id)
        {
            var response = new Response<Funcionario>();

            try
            {
                //TODO: Validações
                response.Entity = db.Funcionarios.FirstOrDefault(f => f.Id == id);

                if (response.Entity == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                } else
                {
                    response.Status = HttpStatusCode.OK;
                }

                await db.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);
                await db.SaveChangesAsync();

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

                var funcionarioBD = db.Funcionarios.AsNoTracking().FirstOrDefault(f => f.Nome.ToLower().Trim() == funcionarioVM.Nome.ToLower().Trim());

                if (funcionarioBD != null)
                {
                    response.Status = HttpStatusCode.NotAcceptable;
                    response.Message = "Esse usuário já foi cadastrado no banco de dados!";

                    return response;
                }

                var funcionarioModel = mapper.Map<Funcionario>(funcionarioVM);

                db.Funcionarios.Add(funcionarioModel);
                await db.SaveChangesAsync();

                response.Entity = funcionarioVM;

                return response;

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
                var funcionarioBd = db.Funcionarios.AsNoTracking().FirstOrDefault(f => f.Id  == id); 

                if (funcionarioBd == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Funcionario não existe!";
                    return response;

                } else
                {
                    funcionarioBd.Nome = funcionarioVM.Nome;
                    funcionarioBd.Email = funcionarioVM.Email;
                    funcionarioBd.Celular = funcionarioVM.Celular;
                    
                    db.Funcionarios.Update(funcionarioBd);
                    await db.SaveChangesAsync();

                    response.Status = HttpStatusCode.OK;
                    response.Message = "Esse usuário foi atualizado no banco de dados!";

                    var map = mapper.Map<FuncionarioViewModel>(funcionarioBd);

                    response.Entity = map;
                    return response;
                }

            } catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao atualizar o funcionario : " + e.Message;

            }
                return response;

        }
        public async Task<Response<FuncionarioViewModel>> Remover(int id)
        {
            var response = new Response<FuncionarioViewModel>();

            var PegarPorid = db.Funcionarios.AsNoTracking().FirstOrDefault(f => f.Id == id);
            try
            {
                if (PegarPorid == null) {
                    response.Status = HttpStatusCode.NotFound;
                } else {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Usuario excluido com sucesso!";

                var mapeamento = mapper.Map<Funcionario>(PegarPorid);
                var mapreverse = mapper.Map<FuncionarioViewModel>(mapeamento);

                db.Funcionarios.Remove(mapeamento);
                db.SaveChanges();

                response.Entity = mapreverse;

                return response;

                }

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
