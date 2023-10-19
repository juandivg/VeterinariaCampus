using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;


public class TratamientoRepository : GenericRepository<Tratamiento>, ITratamientoRepository
{
    private readonly VeterinariaCampusContext _context;
    public TratamientoRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }
            public override async Task<(int totalRegistros, IEnumerable<Tratamiento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Tratamientos as IQueryable<Tratamiento>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Cita.Mascota.Nombre.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
