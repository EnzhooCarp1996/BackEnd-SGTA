using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Models;

namespace BackEndSGTA.Data.MySql;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        // Ignoramos las clases que solo se usan en MongoDB
        modelBuilder.Ignore<Presupuesto>();
        modelBuilder.Ignore<PresupuestoItem>();
    }
}

