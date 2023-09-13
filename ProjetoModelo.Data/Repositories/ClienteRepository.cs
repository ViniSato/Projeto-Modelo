using ProjetoModelo.Data.Context;
using ProjetoModelo.Domain.Interfaces.Repositories;
using ProjetoModelo.Domain.Models;
using ProjetoModelo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ProjetoModelo.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(HPEContext context) : base(context)
        {

        }

        public async Task<Cliente> GetById(int id) => await _context.Cliente.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Cliente> GetByCpf(string dado)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(s => s.CPF == dado);
            return cliente;
        }
        public async Task<Cliente> GetByEmail(string dado)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(s => s.Email == dado);
            return cliente;
        }
        public async Task<Cliente> Add(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return cliente; 
        }
    }
}
