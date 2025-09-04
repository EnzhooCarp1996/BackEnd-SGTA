using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndSGTA.Models.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("cliente");

        builder.HasKey(c => c.IdCliente);

        builder.Property(c => c.IdCliente)
               .HasColumnName("id_cliente");

        builder.Property(c => c.TipoCliente)
               .HasColumnName("tipo_cliente")
               .IsRequired()
               .HasConversion<string>();

        builder.Property(c => c.Telefono)
               .HasColumnName("telefono")
               .HasMaxLength(15);

        builder.Property(c => c.Celular)
               .HasColumnName("celular")
               .HasMaxLength(15);

        builder.Property(c => c.Responsabilidad)
               .HasColumnName("responsabilidad")
               .IsRequired()
               .HasConversion<string>();

        builder.Property(c => c.TipoDocumento)
               .HasColumnName("tipo_documento")
               .IsRequired()
               .HasConversion<string>();

        builder.Property(c => c.Documento)
               .HasColumnName("documento")
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(c => c.Nombre)
               .HasColumnName("nombre")
               .HasMaxLength(40);

        builder.Property(c => c.Apellido)
               .HasColumnName("apellido")
               .HasMaxLength(30);

        builder.Property(c => c.RazonSocial)
               .HasColumnName("razon_social")
               .HasMaxLength(50);

        builder.Property(c => c.NombreDeFantasia)
               .HasColumnName("nombre_de_fantasia")
               .HasMaxLength(50);

        // Configurar herencia TPH
        builder.HasDiscriminator(c => c.TipoCliente)
               .HasValue<Persona>(Cliente.TipoDeCliente.Persona)
               .HasValue<Empresa>(Cliente.TipoDeCliente.Empresa);
    }
}
