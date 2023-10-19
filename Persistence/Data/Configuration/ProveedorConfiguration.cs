using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("Proveedor");
        builder.Property(p=>p.Nombre).HasColumnName("Nombre").HasMaxLength(250).IsRequired();
        builder.Property(p=>p.Direccion).HasColumnName("Direccion").IsRequired();
        builder.Property(p=>p.Telefono).HasColumnName("Telefono").HasMaxLength(250).IsRequired();
        builder.
        HasMany(p=>p.Medicamentos)
        .WithMany(p=>p.Proveedores)
        .UsingEntity<MedicamentoProveedor>(
            j=>j.HasOne(p=>p.Medicamento).WithMany(p=>p.MedicamentoProveedores).HasForeignKey(p=>p.IdMedicamentofk),
            j=>j.HasOne(p=>p.Proveedor).WithMany(p=>p.MedicamentoProveedores).HasForeignKey(p=>p.IdProveedorfk),
            j=>{
                j.HasKey(p=>new{p.IdProveedorfk, p.IdMedicamentofk});
            }
        );



    }
    
}
