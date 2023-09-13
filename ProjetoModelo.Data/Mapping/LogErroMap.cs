using ProjetoModelo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoModelo.Mapping
{
    public static  class LogErroMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogErro>()
                .ToTable("TB_MIT_BRINDE_LOG_ERRO");

            modelBuilder.Entity<LogErro>()
                .HasKey(x => x.IdLogErro);

            modelBuilder.Entity<LogErro>()
                .Property(x => x.IdLogErro)
                .HasColumnName("ID_LOG_ERRO");

            modelBuilder.Entity<LogErro>()
                .Property(x => x.IdUsuario)
                .HasColumnName("ID_CLIENTE");

            modelBuilder.Entity<LogErro>()
                .Property(x => x.DataEvento)
                .HasColumnName("DT_EVENTO");

            modelBuilder.Entity<LogErro>()
                .Property(x => x.Controller)
                .HasColumnName("CONTROLLER")
                .HasMaxLength(50);

            modelBuilder.Entity<LogErro>()
                .Property(x => x.Action)
                .HasColumnName("ACTION")
                .HasMaxLength(50);

            modelBuilder.Entity<LogErro>()
                .Property(x => x.Params)
                .HasColumnName("PARAMS")
                .HasMaxLength(8000);

            modelBuilder.Entity<LogErro>()
                .Property(x => x.Mensagem)
                .HasColumnName("MENSAGEM");

            modelBuilder.Entity<LogErro>()
                .Property(x => x.StackTrace)
                .HasColumnName("STACKTRACE")
                .HasMaxLength(8000);

            modelBuilder.Entity<LogErro>()
                .Property(x => x.Method)
                .HasColumnName("METHOD")
                .HasMaxLength(10);

            modelBuilder.Entity<LogErro>()
                .Property(x => x.IP)
                .HasColumnName("IP")
                .HasMaxLength(50);

            modelBuilder.Entity<LogErro>()
                .Property(x => x.UserAgent)
                .HasColumnName("USER_AGENT")
                .HasMaxLength(1000);

        }
    }
}
