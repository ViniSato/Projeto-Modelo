using ProjetoModelo.Domain.Interfaces.Repositories;
using ProjetoModelo.Domain.Models;
using ProjetoModelo.Data.Context;
using ProjetoModelo.Repositories;

namespace ProjetoModelo.Data.Repositories
{
    public class LogAcessoRepository : BaseRepository<LogAcesso>, ILogAcessoRepository
    {
        public LogAcessoRepository(HPEContext context) : base(context)
        {
        }
    }
}
