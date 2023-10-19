using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamentoRepository
{
    private readonly VeterinariaCampusContext _context;
    public MedicamentoRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }
    public override async Task<IEnumerable<Medicamento>> GetAllAsync()
    {
        return await _context.Medicamentos.Include(p=>p.Laboratorio).ToListAsync();
    }

    public async Task<IEnumerable<Medicamento>> GetMedicamentosLaboratorio(string laboratorio)
    {
        return await _context.Medicamentos.Include(p=>p.Laboratorio).Where(p=>p.Laboratorio.Nombre==laboratorio).ToListAsync();
    }

    public async Task<IEnumerable<Medicamento>> GetMedicamentosxPrecio(decimal precio)
    {
        return await _context.Medicamentos.Where(p=>p.Precio>=precio).ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<Medicamento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicamentos as IQueryable<Medicamento>;
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
