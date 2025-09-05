using BackEndSGTA.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndSGTA.Models.Configurations;

public class PresupuestoConfiguration : IEntityTypeConfiguration<Presupuesto>
{
        public void Configure(EntityTypeBuilder<Presupuesto> builder)
        {
                builder.ToTable("presupuesto");

                builder.HasKey(f => f.IdPresupuesto);

                builder.Property(f => f.IdPresupuesto)
                        .IsRequired()
                        .HasColumnName("id_presupuesto");

                builder.Property(f => f.Fecha)
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                builder.Property(f => f.ManoDeObraChapa)
                        .IsRequired()
                        .HasColumnName("mano_de_obra_chapa");

                builder.Property(f => f.ManoDeObraPintura)
                        .HasColumnName("mano_de_obra_pintura");

                builder.Property(f => f.TotalRepuestos)
                        .HasColumnName("total_repuestos");

                builder.Property(f => f.IdCliente)
                        .IsRequired()
                        .HasColumnName("id_cliente");

                builder.HasOne(f => f.Cliente)
                        .WithMany(c => c.Presupuestos)
                        .HasForeignKey(f => f.IdCliente);


        }
}
