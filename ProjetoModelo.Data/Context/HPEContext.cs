using Microsoft.EntityFrameworkCore;
using ProjetoModelo.Domain.Models;
using ProjetoModelo.Mapping;

namespace ProjetoModelo.Data.Context
{
    public class HPEContext : DbContext
    {
        public HPEContext(DbContextOptions<HPEContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClienteMap.Map(modelBuilder);
            LogAcessoMap.Map(modelBuilder);
            LogErroMap.Map(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<LogErro> LogErro { get; set; }
        public DbSet<LogAcesso> LogAcesso { get; set; }

    }
}
