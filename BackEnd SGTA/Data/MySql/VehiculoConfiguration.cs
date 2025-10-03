using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;

namespace BackEndSGTA.Data;

public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
       public void Configure(EntityTypeBuilder<Vehiculo> builder)
       {
              builder.ToTable(Mensajes.MensajesVehiculos.TABLAVEHICULO);

              builder.HasKey(v => v.IdVehiculo);

              builder.Property(v => v.IdVehiculo)
                     .ValueGeneratedOnAdd()
                     .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_ID_VEHICULO);

              builder.Property(v => v.Patente)
                     .IsRequired()
                     .HasMaxLength(Mensajes.MensajesVehiculos.MAXDIEZ)
                     .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_PATENTE);

              builder.Property(v => v.Marca)
                     .IsRequired()
                     .HasMaxLength(Mensajes.MensajesVehiculos.MAXVEINTE)
                     .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_MARCA);


              builder.Property(v => v.Modelo)
                     .IsRequired()
                     .HasMaxLength(Mensajes.MensajesVehiculos.MAXTREINTA)
                     .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_MODELO);

              builder.Property(v => v.Anio)
                     .IsRequired()
                     .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_ANIO);

              builder.Property(v => v.NroDeChasis)
                     .IsRequired()
                     .HasMaxLength(30)
                     .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_NRO_DE_CHASIS);

              builder.Property(v => v.Estado)
                     .HasColumnType(Mensajes.MensajesVehiculos.TIPOCOLUMNAENUM)
                     .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_ESTADO);

              builder.Property(v => v.FechaRecibido)
                    .HasColumnType(Mensajes.MensajesVehiculos.TIPOCOLUMNADATE)
                    .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_FECHA_RECIBIDO);

              builder.Property(v => v.FechaEsperada)
                    .HasColumnType(Mensajes.MensajesVehiculos.TIPOCOLUMNADATE)
                    .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_FECHA_ESPERADA);

              builder.Property(v => v.FechaEntrega)
                    .HasColumnType(Mensajes.MensajesVehiculos.TIPOCOLUMNADATE)
                    .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_FECHA_ENTREGA);

              builder.Property(v => v.DescripcionTrabajos)
                    .HasColumnType(Mensajes.MensajesVehiculos.TIPOCOLUMNATEXT)
                    .HasColumnName(Mensajes.MensajesVehiculos.CAMPO_DESCRIPCION_TRABAJOS);

              builder.Property(v => v.IdCliente)
                    .HasColumnName(Mensajes.MensajesClientes.CAMPO_ID_CLIENTE);

              builder.HasOne(v => v.Cliente)
                     .WithMany(c => c.Vehiculos)
                     .HasForeignKey(v => v.IdCliente)
                     .OnDelete(DeleteBehavior.SetNull);
       }
}

