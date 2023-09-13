using ProjetoModelo.Data.Repositories;
using ProjetoModelo.Domain.Interfaces.Repositories;

namespace ProjetoModelo.IoC.Modules
{
    public class RepositoryModule
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<ILogErroRepository, LogErroRepository>();
            services.AddTransient<ILogAcessoRepository, LogAcessoRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
        }
    }
}
