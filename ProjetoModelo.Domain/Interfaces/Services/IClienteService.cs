using ProjetoModelo.Domain.Models;
using ProjetoModelo.Domain.Models.ViewModels;

namespace ProjetoModelo.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteViewModel> GetById(int id);
        Task<bool> ValidaCliente(string dado, string tipo);
        Task<bool> Validador(string cpf, string email);
        Task<ClienteViewModel> CadastrarCliente(Cliente novoCliente);
    }
}
