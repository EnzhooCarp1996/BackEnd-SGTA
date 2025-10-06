using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;

namespace BackEndSGTA.Data.MySql;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
       public void Configure(EntityTypeBuilder<Cliente> builder)
       {
              builder.ToTable(Mensajes.MensajesClientes.TABLACLIENTE);

              builder.HasKey(c => c.IdCliente);

              builder.Property(c => c.IdCliente)
                     .ValueGeneratedOnAdd()
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_ID_CLIENTE);

              builder.Property(c => c.TipoCliente)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_TIPO_CLIENTE)
                     .IsRequired()
                     .HasConversion<string>();

              builder.Property(c => c.Telefono)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_TELEFONO)
                     .HasMaxLength(15);

              builder.Property(c => c.Celular)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_CELULAR)
                     .HasMaxLength(15);

              builder.Property(c => c.Responsabilidad)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_RESPONSABILIDAD)
                     .IsRequired()
                     .HasConversion<string>();

              builder.Property(c => c.TipoDocumento)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_TIPO_DOCUMENTO)
                     .IsRequired()
                     .HasConversion<string>();

              builder.Property(c => c.Documento)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_DOCUMENTO)
                     .HasMaxLength(Mensajes.MensajesClientes.MAXVEINTE)
                     .IsRequired();

              builder.Property(c => c.Nombre)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_NOMBRE)
                     .HasMaxLength(Mensajes.MensajesClientes.MAXCUARENTA);

              builder.Property(c => c.Apellido)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_APELLIDO)
                     .HasMaxLength(Mensajes.MensajesClientes.MAXTREINTA);

              builder.Property(c => c.RazonSocial)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_RAZON_SOCIAL)
                     .HasMaxLength(Mensajes.MensajesClientes.MAXCINCUENTA);

              builder.Property(c => c.NombreDeFantasia)
                     .HasColumnName(Mensajes.MensajesClientes.CAMPO_NOMBRE_DE_FANTASIA)
                     .HasMaxLength(Mensajes.MensajesClientes.MAXCINCUENTA);


       }
}
