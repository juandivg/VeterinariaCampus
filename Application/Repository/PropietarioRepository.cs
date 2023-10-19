using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PropietarioRepository : GenericRepository<Propietario>, IPropietarioRepository
{
    private readonly VeterinariaCampusContext _context;
    public PropietarioRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }
    public override async Task<IEnumerable<Propietario>> GetAllAsync()
    {
        return await _context.Propietarios.ToListAsync();
    }

    public async Task<IEnumerable<Propietario>> GetPropietarioxMascotas()
    {
        return await _context.Propietarios.Include(p=>p.Mascotas).ToListAsync();
    }

    public async Task<IEnumerable<Propietario>> GetPropietarioxMascotasRaza(string raza)
    {
        return await _context.Propietarios.Include(p=>p.Mascotas.Where(p=>p.Raza.Nombre==raza)).Where(p=>p.Mascotas.Where(p=>p.Raza.Nombre==raza).Count()>0).ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<Propietario> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Propietarios as IQueryable<Propietario>;
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
