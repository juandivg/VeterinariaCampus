using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class PropietarioConfiguration : IEntityTypeConfiguration<Propietario>
    {
        public void Configure(EntityTypeBuilder<Propietario> builder)
        {
            builder.ToTable("Propietario");
            builder.Property(p=>p.Nombre).HasColumnName("Nombre").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Email).HasColumnName("Email").IsRequired();
            builder.Property(p=>p.Telefono).HasColumnName("Telefono").IsRequired();
            

        }
    }
