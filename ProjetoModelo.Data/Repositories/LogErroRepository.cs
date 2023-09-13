using ProjetoModelo.Domain.Interfaces.Repositories;
using ProjetoModelo.Domain.Models;
using ProjetoModelo.Data.Context;
using ProjetoModelo.Repositories;

namespace ProjetoModelo.Data.Repositories
{
    public class LogErroRepository : BaseRepository<LogErro>, ILogErroRepository
    {
        public LogErroRepository(HPEContext context) : base(context)
        {
        }
    }
}
