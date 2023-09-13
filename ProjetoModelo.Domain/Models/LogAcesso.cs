namespace ProjetoModelo.Domain.Models
{
    public class LogAcesso
    {
        public int IdLogAcesso { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? DataAcesso { get; set; }
        public string Acao { get; set; }
        public string? Detalhe { get; set; }
        public string? Ip { get; set; }
        public string? HostName { get; set; }
        public string? UserAgent { get; set; }
    }
}
