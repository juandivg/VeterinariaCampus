using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("cita");
        builder.Property(p=>p.Fecha).HasColumnName("FechaCita").HasMaxLength(250).IsRequired();
        builder.Property(p=>p.Motivo).HasColumnName("Motivo").HasMaxLength(250).IsRequired();
        builder.HasOne(p=>p.Mascota).WithMany(p=>p.Citas).HasForeignKey(p=>p.IdMascotafk);
        builder.HasOne(p=>p.Veterinario).WithMany(p=>p.Citas).HasForeignKey(p=>p.IdVeterinariofk);
    }
}
