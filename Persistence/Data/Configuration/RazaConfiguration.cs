using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class RazaConfiguration : IEntityTypeConfiguration<Raza>
{
    public void Configure(EntityTypeBuilder<Raza> builder)
    {
        builder.ToTable("Raza");
        builder.Property(p=>p.Nombre).HasColumnName("Nombre").HasMaxLength(250).IsRequired();
        builder.HasOne(p=>p.Especie).WithMany(p=>p.Razas).HasForeignKey(p=>p.IdEspeciefk);
    }
}
