using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Helpers;

namespace BackEndSGTA.Models.Configurations;

public class PresupuestoConfiguration : IEntityTypeConfiguration<Presupuesto>
{
        public void Configure(EntityTypeBuilder<Presupuesto> builder)
        {
                builder.ToTable(Mensajes.MensajesPresupuestos.TABLA_PRESUPUESTO);

                builder.HasKey(p => p.IdPresupuesto);

                builder.Property(p => p.IdPresupuesto)
                        .IsRequired()
                        .HasColumnName(Mensajes.MensajesPresupuestos.CAMPO_ID_PRESUPUESTO);

                builder.Property(p => p.Fecha)
                        .HasColumnType(Mensajes.MensajesPresupuestos.TIPO_DATE)
                        .HasColumnName(Mensajes.MensajesPresupuestos.CAMPO_FECHA);

                builder.Property(p => p.ManoDeObraChapa)
                        .IsRequired()
                        .HasColumnName(Mensajes.MensajesPresupuestos.CAMPO_MANO_DE_OBRA_CHAPA);

                builder.Property(p => p.ManoDeObraPintura)
                        .HasColumnName(Mensajes.MensajesPresupuestos.CAMPO_MANO_DE_OBRA_PINTURA);

                builder.Property(p => p.TotalRepuestos)
                        .HasColumnName(Mensajes.MensajesPresupuestos.CAMPO_TOTAL_REPUESTOS);

                builder.Property(p => p.IdCliente)
                        .HasColumnName(Mensajes.MensajesClientes.CAMPO_ID_CLIENTE);

                builder.HasOne(p => p.Cliente)
                        .WithMany(c => c.Presupuestos)
                        .HasForeignKey(p => p.IdCliente)
                        .OnDelete(DeleteBehavior.SetNull);


        }
}
