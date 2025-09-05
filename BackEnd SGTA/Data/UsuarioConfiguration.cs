using BackEndSGTA.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndSGTA.Models.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
                builder.ToTable("usuario");

                builder.HasKey(c => c.IdUsuario);

                builder.Property(c => c.IdUsuario)
                        .IsRequired()
                        .HasColumnName("id_usuario");

                builder.Property(c => c.NombreUsuario)
                        .IsRequired()
                        .HasColumnName("nombre_usuario");

                builder.Property(c => c.Correo)
                        .IsRequired()
                        .HasColumnName("correo");

                builder.Property(u => u.Tipo)
                        .IsRequired()
                        .HasColumnType("enum('Admin', 'Encargado', 'Empleado')")
                        .HasColumnName("tipo");

                builder.Property(c => c.Contrasenia)
                        .IsRequired()
                        .HasColumnName("contrasenia");

        }
}
