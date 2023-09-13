using ProjetoModelo.Domain.Models;

namespace ProjetoModelo.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<Cliente> GetById(int id);
        Task<Cliente> GetByCpf(string dado);
        Task<Cliente> GetByEmail(string dado);

        Task<Cliente> Add(Cliente cliente);
    }
}
