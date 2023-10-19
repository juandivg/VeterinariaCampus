using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
    {
        public void Configure(EntityTypeBuilder<Mascota> builder)
        {
            builder.ToTable("Mascota");
            builder.Property(p=>p.Nombre).HasColumnName("Nombre").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.FechaNacimiento).HasColumnName("fecha").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.Propietario).WithMany(p=>p.Mascotas).HasForeignKey(p=>p.IdPropetiariofk);
            builder.HasOne(p=>p.Raza).WithMany(p=>p.Mascotas).HasForeignKey(p=>p.IdRazafk);


        }
    }
