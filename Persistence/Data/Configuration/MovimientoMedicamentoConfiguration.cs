using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MovimientoMedicamentoConfiguration : IEntityTypeConfiguration<MovimientoMedicamento>
    {
        public void Configure(EntityTypeBuilder<MovimientoMedicamento> builder)
        {
            builder.ToTable("MovimientoMedicamento");
            builder.Property(p=>p.Fecha).HasColumnName("Fecha").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Cantidad).HasColumnName("Cantidad").HasColumnType("int").IsRequired();
            builder.HasOne(p=>p.Usuario).WithMany(p=>p.MovimientoMedicamentos).HasForeignKey(p=>p.IdUsuariofk);
            builder.HasOne(p=>p.Propietario).WithMany(p=>p.MovimientoMedicamentos).HasForeignKey(p=>p.IdPropetiariofk);
            builder.HasOne(p=>p.TipoMovimiento).WithMany(p=>p.MovimientoMedicamentos).HasForeignKey(p=>p.IdTipoMovimientofk);
            builder.HasOne(p=>p.Medicamento).WithMany(p=>p.MovimientoMedicamentos).HasForeignKey(p=>p.IdMedicamentofk);
        }
    }
}