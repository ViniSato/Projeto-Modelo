namespace ProjetoModelo.Domain.Models
{
    public class LogErro
    {
        public int IdLogErro { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? DataEvento { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? Params { get; set; }
        public string? Mensagem { get; set; }
        public string? StackTrace { get; set; }
        public string? Method { get; set; }
        public string? IP { get; set; }
        public string? UserAgent { get; set; }
    }
}
