using ProjetoModelo.Middleware;

namespace ProjetoModelo.IoC.Modules
{
    public class MiddlewareModule
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<ErrorHandlingMiddleware>();
        }
    }
}
