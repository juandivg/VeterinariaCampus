using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class RazaRepository : GenericRepository<Raza>, IRazaRepository
{
    private readonly VeterinariaCampusContext _context;
    public RazaRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }
    public override async Task<IEnumerable<Raza>> GetAllAsync()
    {
        return await _context.Razas.ToListAsync();
    }

    public async Task<IEnumerable<Raza>> GetRazasxMascotas()
    {
        return await _context.Razas.Include(p=>p.Mascotas).ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<Raza> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Razas as IQueryable<Raza>;
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
