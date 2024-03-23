using Joshua.Application.Business.Interfaces;
using Joshua.Application.Business;

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
