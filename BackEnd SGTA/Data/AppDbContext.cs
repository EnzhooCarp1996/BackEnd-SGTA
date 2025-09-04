using BackEndSGTA.Models;
using Microsoft.EntityFrameworkCore;
using static BackEndSGTA.Models.Cliente;

namespace BackEndSGTA.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Factura> Facturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.NombreUsuario).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Contrasenia).IsRequired();
                entity.Property(e => e.Tipo).IsRequired();
            });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Factura
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.ToTable("Factura");
            entity.HasKey(e => e.IdFactura);
            entity.Property(e => e.Fecha).IsRequired();
            entity.HasOne(e => e.Cliente)
                  .WithMany(c => c.Facturas)
                  .HasForeignKey(e => e.IdCliente);
        });

    }
}

