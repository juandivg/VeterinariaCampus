using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
{
    public void Configure(EntityTypeBuilder<Medicamento> builder)
    {
        builder.ToTable("Medicamento");
        builder.Property(p=>p.Nombre).HasColumnName("Nombre").HasMaxLength(250).IsRequired();
        builder.Property(p => p.Stock).HasColumnName("Stock").HasColumnType("int").IsRequired();
        builder.Property(p => p.Precio).HasColumnName("Precio").HasColumnType("decimal").IsRequired();
        builder.HasOne(p=>p.Laboratorio).WithMany(p=>p.Medicamentos).HasForeignKey(p=>p.IdLaboratoriofk);

    }
}
