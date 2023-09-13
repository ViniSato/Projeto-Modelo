namespace ProjetoModelo.Domain.Interfaces.Services
{
    public interface ILogAcessoService
    {
        Task Save(string acao, string detalhe, int? idUsuario);
    }
}
