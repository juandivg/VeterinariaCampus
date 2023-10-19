using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class VeterinarioRepository : GenericRepository<Veterinario>, IVeterinarioRepository
{
    private readonly VeterinariaCampusContext _context;
    public VeterinarioRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }

    public async Task<IEnumerable<Veterinario>> GetVeterinariosEspecialidad(string especialidad)
    {
        return await _context.Veterinarios.Where(p=>p.Especialidad==especialidad).ToListAsync();
    }
    public override async Task<IEnumerable<Veterinario>> GetAllAsync()
    {
        return await _context.Veterinarios.ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<Veterinario> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Veterinarios as IQueryable<Veterinario>;
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
