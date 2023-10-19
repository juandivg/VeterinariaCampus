using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository
{
    private readonly VeterinariaCampusContext _context;
    public ProveedorRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }
    public override async Task<IEnumerable<Proveedor>> GetAllAsync()
    {
        return await _context.Proveedores.ToListAsync();
    }

    public async Task<IEnumerable<Proveedor>> GetProveedoresxMedicamento(string medicamento)
    {
        return await _context.Proveedores.Where(p=>p.MedicamentoProveedores.Where(p=>p.Medicamento.Nombre==medicamento).Count()>0).ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<Proveedor> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Proveedores as IQueryable<Proveedor>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
