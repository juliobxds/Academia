using Joshua.Application.Business;
using Joshua.Application.Business.Interfaces;

namespace Joshua.API.Configurations
{
    public static class ServicesConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFuncionarioBusiness, FuncionarioBusiness>();
            
        }
    }
}
