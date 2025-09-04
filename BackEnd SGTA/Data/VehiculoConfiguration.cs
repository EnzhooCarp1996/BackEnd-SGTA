using BackEndSGTA.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndSGTA.Models.Configurations
{
       public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
       {
              public void Configure(EntityTypeBuilder<Vehiculo> builder)
              {
                     builder.ToTable("vehiculo");

                     builder.HasKey(v => v.IdVehiculo);

                     builder.Property(v => v.IdVehiculo)
                            .HasColumnName("id_vehiculo");

                     builder.Property(v => v.Patente)
                            .IsRequired()
                            .HasMaxLength(Mensajes.MAXDIEZ)
                            .HasColumnName("patente");

                     builder.Property(v => v.Marca)
                            .IsRequired()
                            .HasMaxLength(Mensajes.MAXVEINTE)
                            .HasColumnName("marca");


                     builder.Property(v => v.Modelo)
                            .IsRequired()
                            .HasMaxLength(Mensajes.MAXTREINTA)
                            .HasColumnName("modelo");

                     builder.Property(v => v.Anio)
                            .IsRequired()
                            .HasColumnType("year")
                            .HasColumnName("anio");

                     builder.Property(v => v.NroDeChasis)
                            .IsRequired()
                            .HasMaxLength(30)
                            .HasColumnName("nro_de_chasis");

                     builder.Property(v => v.Estado)
                            .HasColumnType("enum('Recibido','No Recibido','Proceso','Entregado')")
                            .HasDefaultValue("No Recibido")
                            .HasColumnName("estado");

                     builder.Property(e => e.FechaRecibido)
                           .HasColumnType("date")
                           .HasColumnName("fecha_recibido");

                     builder.Property(e => e.FechaEsperada)
                           .HasColumnType("date")
                           .HasColumnName("fecha_esperada");

                     builder.Property(e => e.FechaEntrega)
                           .HasColumnType("date")
                           .HasColumnName("fecha_entrega");

                     builder.Property(e => e.DescripcionTrabajos)
                           .HasColumnType("text")
                           .HasColumnName("descripcion_trabajos");

                     builder.Property(e => e.IdCliente)
                           .IsRequired()
                           .HasColumnName("id_cliente");

                     builder.HasOne(v => v.Cliente)
                            .WithMany(c => c.Vehiculos)
                            .HasForeignKey(v => v.IdCliente)
                            .OnDelete(DeleteBehavior.Cascade);
              }
       }
}
