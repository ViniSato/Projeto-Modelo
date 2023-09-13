using ProjetoModelo.Domain.Interfaces.Services;
using ProjetoModelo.Services.AppServices;
using ProjetoModelo.Services.BaseServices;

namespace ProjetoModeloApi.IoC.Modules
{
    public class ServiceModule
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<ILogErroService, LogErroService>();
            services.AddTransient<ILogAcessoService, LogAcessoService>();
            services.AddTransient<IClienteService, ClienteService>();
        }
    }
}
