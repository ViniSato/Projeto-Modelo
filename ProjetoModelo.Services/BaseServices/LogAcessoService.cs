using ProjetoModelo.Domain.Interfaces.Repositories;
using ProjetoModelo.Domain.Interfaces.Services;
using ProjetoModelo.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Serilog;
using ProjetoModelo.Domain.Helpers;

namespace ProjetoModelo.Services.BaseServices
{
    public class LogAcessoService : ILogAcessoService
    {
        private readonly ILogAcessoRepository _logAcessoRepository;
        private readonly IHttpContextAccessor _accessor;

        public LogAcessoService(ILogAcessoRepository logAcessoRepository,
                                IHttpContextAccessor accessor)
        {
            _logAcessoRepository = logAcessoRepository;
            _accessor = accessor;
        }

        public async Task Save(string acao, string detalhe, int? idUsuario)
        {
            try
            {
                var register = new LogAcesso
                {
                    IdUsuario = idUsuario,
                    Acao = acao,
                    Detalhe = detalhe,
                    Ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    HostName = UtilsHelper.GetHostName(_accessor.HttpContext),
                    UserAgent = _accessor.HttpContext.Request.Headers.ContainsKey("User-Agent") ? _accessor.HttpContext.Request.Headers[HeaderNames.UserAgent].ToString() : null,
                    DataAcesso = DateTime.Now
                };

                Log.Information("[Ação: {Acao}] [Detalhe: {Detalhe}] [Usuario: {ID}] [IP: {IP}] [Hostname: {HostName}]",
                    register.Acao, register.Detalhe, register.IdUsuario, register.Ip, register.HostName);

                await _logAcessoRepository.Add(register);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "[Ação: {Acao}][Exceção: {Mensagem}]", "Erro ao salvar log de acesso", exception.Message);
            }
        }
    }
}
