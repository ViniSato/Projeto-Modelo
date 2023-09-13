using ProjetoModelo.Domain.Models;

namespace ProjetoModelo.Domain.Interfaces.Services
{
    public interface ILogErroService
    {
        Task<LogErro> Save(Exception ex, bool enviarBot, int? idUsuario);
        Task<LogErro> Save(Exception ex, bool enviarBot, int? idUsuario, string? body);
        Task SendErrorBot(LogErro logErro);
    }
}
