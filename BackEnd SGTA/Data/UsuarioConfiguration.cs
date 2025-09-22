using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Helpers;

namespace BackEndSGTA.Models.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
                builder.ToTable(Mensajes.MensajesUsuarios.TABLA_USUARIO);

                builder.HasKey(c => c.IdUsuario);

                builder.Property(c => c.IdUsuario)
                        .IsRequired()
                        .HasColumnName(Mensajes.MensajesUsuarios.CAMPO_ID_USUARIO);

                builder.Property(c => c.NombreUsuario)
                        .IsRequired()
                        .HasColumnName(Mensajes.MensajesUsuarios.CAMPO_NOMBRE_USUARIO);

                builder.Property(c => c.Correo)
                        .IsRequired()
                        .HasColumnName(Mensajes.MensajesUsuarios.CAMPO_CORREO);

                builder.Property(u => u.Role)
                        .IsRequired()
                        .HasColumnType(Mensajes.MensajesUsuarios.COLUMNTYPE_ENUM)
                        .HasColumnName(Mensajes.MensajesUsuarios.CAMPO_ROL);

                builder.Property(c => c.Contrasenia)
                        .IsRequired()
                        .HasColumnName(Mensajes.MensajesUsuarios.CAMPO_CONTRASENIA);

        }
}
