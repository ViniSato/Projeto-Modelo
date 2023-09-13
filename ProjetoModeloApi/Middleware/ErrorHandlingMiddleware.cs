using ProjetoModelo.Domain.Exceptions;
using ProjetoModelo.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace ProjetoModelo.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogAcessoService _logAcessoService;
        private readonly ILogErroService _logErroService;

        public ErrorHandlingMiddleware(ILogAcessoService logAcessoService, ILogErroService logErroService)
        {
            _logAcessoService = logAcessoService;
            _logErroService = logErroService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                context.Request.EnableBuffering();
                await next(context);
            }
            catch (NotFoundException notFoundException)
            {
                await HandleNotFoundException(context, notFoundException);
            }
            catch (BadRequestException registroErroException)
            {
                await HandleBadRequestException(context, registroErroException);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }
        private async Task HandleBadRequestException(HttpContext context, BadRequestException exception)
        {
            var body = await GetBody(context);

            await _logErroService.Save(exception, false, null);

            var contentType = "application/json";
            var statusCode = StatusCodes.Status400BadRequest;

            var message = exception.Message;
            if (exception.Erros != null)
            {
                foreach (var erro in exception.Erros)
                {
                    message += $"\n- {erro};";
                }
            }

            var response = JsonConvert.SerializeObject(message);

            context.Response.ContentType = contentType;
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private async Task HandleNotFoundException(HttpContext context, NotFoundException exception)
        {
            var body = await GetBody(context);

            await _logErroService.Save(exception, false, null, body);

            var contentType = "application/json";
            var statusCode = StatusCodes.Status404NotFound;

            var response = JsonConvert.SerializeObject(exception.Message);

            context.Response.ContentType = contentType;
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var body = await GetBody(context);

            await _logErroService.Save(exception, true, null, body);

            var contentType = "application/json";
            var statusCode = StatusCodes.Status500InternalServerError;

            var response = JsonConvert.SerializeObject(exception.Message);

            context.Response.ContentType = contentType;
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private string GetController(HttpContext context)
        {
            return context.GetRouteData().Values["controller"].ToString();
        }

        private string GetAction(HttpContext context)
        {
            return context.GetRouteData().Values["action"].ToString();

        }

        private async Task<string?> GetBody(HttpContext context)
        {
            context.Request.Body.Position = 0;
            string bodyString = await new StreamReader(context.Request.Body).ReadToEndAsync();

            return bodyString;
        }

    }
}
