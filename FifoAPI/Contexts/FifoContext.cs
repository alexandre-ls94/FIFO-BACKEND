using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FifoAPI.Domains
{
    public partial class FifoContext : DbContext
    {
        public FifoContext()
        {
        }

        public FifoContext(DbContextOptions<FifoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Atividade> Atividade { get; set; }
        public virtual DbSet<Fila> Fila { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-PMKAL0V7\\SQLEXPRESS; Initial Catalog=Fifo; Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atividade>(entity =>
            {
                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Atividad__7B406B561E581E49")
                    .IsUnique();

                entity.Property(e => e.JogadoresPorVez).HasDefaultValueSql("((1))");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fila>(entity =>
            {
                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAtividadeNavigation)
                    .WithMany(p => p.Fila)
                    .HasForeignKey(d => d.IdAtividade)
                    .HasConstraintName("FK__Fila__IdAtividad__3C69FB99");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Fila)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Fila__IdUsuario__3D5E1FD2");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Nickname)
                    .HasName("UQ__Usuario__CC6CD17EA972B6DD")
                    .IsUnique();

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });
        }
    }
}
