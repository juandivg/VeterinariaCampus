using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MascotaRepository : GenericRepository<Mascota>, IMascotaRepository
{
    private readonly VeterinariaCampusContext _context;
    public MascotaRepository(VeterinariaCampusContext context) : base(context)
    {
            _context = context;
    }
    public override async Task<IEnumerable<Mascota>> GetAllAsync()
    {
        return await _context.Mascotas.ToListAsync();
    }

    public async Task<IEnumerable<Mascota>> GetMascotasEspecie(string especie)
    {
        return await _context.Mascotas.Include(p=>p.Raza).ThenInclude(p=>p.Especie).Where(p=>p.Raza.Especie.Nombre==especie).ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Mascota> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Mascotas as IQueryable<Mascota>;
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
