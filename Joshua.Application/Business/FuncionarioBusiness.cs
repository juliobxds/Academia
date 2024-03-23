using Joshua.Application.Business.Interfaces;
using Joshua.Domain.Models;
using Joshua.Infra.Data.Data;
using Joshua.Infra.Utils.Transports;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Joshua.Application.Business
{
    public class FuncionarioBusiness : IFuncionarioBusiness
    {
        private readonly JoshuaContext db;

        public FuncionarioBusiness(JoshuaContext db)
        {
            this.db = db;
        }

        public Response<Funcionario> GetById(int id)
        {
            var response = new Response<Funcionario>() ;

            try
            {
                //TODO: Validações
                response.Entity = db.Funcionarios.AsNoTracking().FirstOrDefault(f => f.Id == id);

                if (response.Entity == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                }
            }
            catch (Exception e)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro ao obter funcionario : " + e.Message;
            }

            return response;
        }
    }
}
