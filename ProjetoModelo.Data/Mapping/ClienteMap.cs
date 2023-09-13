using ProjetoModelo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoModelo.Mapping
{
    public static class ClienteMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .ToTable("TB_MIT_BRINDE_CLIENTE");

            modelBuilder.Entity<Cliente>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Id)
                .HasColumnName("ID_CLIENTE");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.CPF)
                .HasMaxLength(50)
                .HasColumnName("CPF");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Nome)
                .HasColumnName("NOME");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Email)
                .HasColumnName("EMAIL");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.DataNascimento)
                .HasColumnName("DT_NASCIMENTO");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Endereco)
                .HasColumnName("ENDERECO");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Telefone)
                .HasColumnName("TELEFONE");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Profissao)
                .HasColumnName("PROFISSAO");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.Comentarios)
                .HasColumnName("COMENTARIOS");

            modelBuilder.Entity<Cliente>()
                .Property(x => x.DataCadastro)
                .HasColumnName("DATA_CADASTRO");
        }
    }
}
