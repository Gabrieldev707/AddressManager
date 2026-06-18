using AddressManager.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressManager.Data;

/// <summary>
/// Contexto do Entity Framework Core que mapeia as tabelas Usuarios e Enderecos.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(u => u.Id);

            // Login deve ser único.
            entity.HasIndex(u => u.Login).IsUnique();
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.ToTable("Enderecos");
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Usuario)
                  .WithMany(u => u.Enderecos)
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
