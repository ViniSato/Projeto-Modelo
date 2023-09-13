using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProjetoModelo.Domain.Models;
using ProjetoModelo.Domain.Interfaces.Repositories;
using ProjetoModelo.Domain.Interfaces.Services;
using Serilog;
using Microsoft.Net.Http.Headers;

namespace ProjetoModelo.Services.BaseServices
{
    public class LogErroService : ILogErroService
    {
        private readonly ILogErroRepository _logErroRepository;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IHttpContextAccessor _accessor;

        public LogErroService(ILogErroRepository logErroRepository,
                               IActionContextAccessor actionContextAccessor,
                               IHttpContextAccessor accessor)
        {
            _logErroRepository = logErroRepository;
            _actionContextAccessor = actionContextAccessor;
            _accessor = accessor;
        }
        private string GetAction()
        {
            var e = _actionContextAccessor.ActionContext.RouteData.Values;

            if (e.Count > 1)
            {
                return (string)e["action"] != null ? (string)e["action"] : "N/A";
            }

            return "N/A";
        }

        private string GetController()
        {
            var e = _actionContextAccessor.ActionContext.RouteData.Values;

            return (string)e["controller"] != null ? (string)e["controller"] : "N/A";
        }

        private string GetErrorMessage(Exception exception)
        {
            try
            {
                string message = string.Empty;

                do
                {
                    message += exception.Message + Environment.NewLine;
                    exception = exception.InnerException;
                }
                while (exception != null);

                return message;
            }
            catch
            {
                return null;
            }
        }

        private string GetMethod()
        {
            return _actionContextAccessor.ActionContext.HttpContext.Request.Method;
        }

        public async Task<LogErro> Save(Exception ex, bool enviarBot, int? idUsuario, string? body)
        {
            try
            {
                LogErro errorLog = new LogErro()
                {
                    DataEvento = DateTime.Now,
                    Controller = GetController(),
                    Action = GetAction(),
                    Method = GetMethod(),
                    Mensagem = GetErrorMessage(ex),
                    UserAgent = _accessor.HttpContext.Request.Headers.ContainsKey("User-Agent") ? _accessor.HttpContext.Request.Headers[HeaderNames.UserAgent].ToString() : null,
                    IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    StackTrace = ex.StackTrace != null ? ex.StackTrace : ex.Source != null ? ex.Source : "-"
                };

                if (enviarBot)
                {
                    await SendErrorBot(errorLog);
                }

                Log.Error(ex, "[Controller: {Controller}] [Ação: {Action}] [Exceção: {Mensagem}] [IP: {IP}] [UserAgent: {UserAgent}] [Usuario: {IdUsuario}] [Body: {Body}]",
                    errorLog.Controller, errorLog.Action, errorLog.Mensagem, errorLog.IP, errorLog.UserAgent, $"{idUsuario}" ?? "--", body);

                await _logErroRepository.Add(errorLog);

                return errorLog;
            }
            catch (Exception exception)
            {
                Log.Error(exception, "[Ação: {Acao}][Exceção: {Mensagem}]", "Erro ao salvar log de erros", exception.Message);
            }

            return null;
        }

        public async Task<LogErro> Save(Exception ex, bool enviarBot, int? idUsuario)
        {
            try
            {
                LogErro errorLog = new LogErro()
                {
                    DataEvento = DateTime.Now,
                    Controller = GetController(),
                    Action = GetAction(),
                    Method = GetMethod(),
                    Mensagem = GetErrorMessage(ex),
                    UserAgent = _accessor.HttpContext.Request.Headers.ContainsKey("User-Agent") ? _accessor.HttpContext.Request.Headers[HeaderNames.UserAgent].ToString() : null,
                    IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    StackTrace = ex.StackTrace != null ? ex.StackTrace : ex.Source != null ? ex.Source : "-"
                };

                if (enviarBot)
                {
                    await SendErrorBot(errorLog);
                }

                Log.Error(ex, "[Controller: {Controller}] [Ação: {Action}] [Exceção: {Mensagem}] [IP: {IP}] [UserAgent: {UserAgent}] [Usuario: {IdUsuario}]",
                    errorLog.Controller, errorLog.Action, errorLog.Mensagem, errorLog.IP, errorLog.UserAgent, $"{idUsuario}" ?? "--");

                await _logErroRepository.Add(errorLog);

                return errorLog;
            }
            catch (Exception exception)
            {
                Log.Error(exception, "[Ação: {Acao}][Exceção: {Mensagem}]", "Erro ao salvar log de erros", exception.Message);
            }

            return null;
        }

        public async Task SendErrorBot(LogErro erro)
        {

            using var client = new HttpClient();
            Assembly assembly = Assembly.GetExecutingAssembly();
            StringBuilder sb = new StringBuilder();


            sb.Append("⚠️⚠️ **ATENÇÃO** ⚠️⚠️ \n\n");
            sb.AppendLine($"APLICAÇÃO: {assembly.FullName}");
            sb.AppendLine($"DATA: {erro.DataEvento}");
            sb.AppendLine($"CONTROLLER: {erro.Controller}");
            sb.AppendLine($"ACTION: {erro.Action}");
            sb.AppendLine($"MENSAGEM: {erro.Mensagem}");
            sb.AppendLine($"URL: {_accessor.HttpContext.Request.Host.ToString()}");
            sb.AppendLine($"USUARIO: {erro.UserAgent}\n");
            sb.AppendLine($"STACKTRACE:\n   {erro.StackTrace}");

            client.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue { NoCache = false };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
         }
    }
}
