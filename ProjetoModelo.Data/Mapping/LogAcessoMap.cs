using ProjetoModelo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoModelo.Mapping
{
    public class LogAcessoMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogAcesso>()
                .ToTable("TB_MIT_BRINDE_LOG_ACESS");

            modelBuilder.Entity<LogAcesso>()
                .HasKey(x => x.IdLogAcesso);

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.IdLogAcesso)
                .HasColumnName("ID_LOG_ACESSO");

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.IdUsuario)
                .HasColumnName("ID_CLIENTE");

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.DataAcesso)
                .HasColumnName("DT_ACESSO");

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.Acao)
                .HasColumnName("ACAO")
                .HasMaxLength(255);

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.Detalhe)
                .HasColumnName("DETALHE")
                .HasMaxLength(8000);

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.Ip)
                .HasColumnName("IP")
                .HasMaxLength(50);

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.HostName)
                .HasColumnName("HOSTNAME")
                .HasMaxLength(255);

            modelBuilder.Entity<LogAcesso>()
                .Property(x => x.UserAgent)
                .HasColumnName("USER_AGENT")
                .HasMaxLength(1000);
        }
    }
}
