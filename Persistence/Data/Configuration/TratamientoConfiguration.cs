using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class TratamientoConfiguration : IEntityTypeConfiguration<Tratamiento>
    {
        public void Configure(EntityTypeBuilder<Tratamiento> builder)
        {
            builder.ToTable("Tratamiento");
            builder.Property(p => p.Dosis).HasColumnName("Dosis").HasColumnType("decimal").IsRequired();
            builder.Property(p => p.FechaAdministracion).HasColumnName("Fecha").IsRequired();
            builder.Property(p => p.Observacion).HasColumnName("Observacion").IsRequired();
            builder.HasOne(p=>p.Cita).WithMany(p=>p.Tratamientos).HasForeignKey(p=>p.IdCitafk);
            builder.HasOne(p=>p.Medicamento).WithMany(p=>p.Tratamientos).HasForeignKey(p=>p.IdMedicamentofk);


        }
    }
