using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class CitaRepository : GenericRepository<Cita>, ICitaRepository
    {
        private readonly VeterinariaCampusContext _context;
        public CitaRepository(VeterinariaCampusContext context) : base(context)
        {
            _context=context;
        }
    public override async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Citas.ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetMascotasCita(string motivo, DateTime fechainicio, DateTime fechafinal)
    {
        return await _context.Citas.Include(p=>p.Mascota).Where(p=>p.Motivo==motivo && p.Fecha>=fechainicio && p.Fecha<=fechafinal).ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetMascotasVeterinario(string veterinario)
    {
        return await _context.Citas.Include(p=>p.Mascota).Where(p=>p.Veterinario.Nombre==veterinario).ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Cita> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Citas as IQueryable<Cita>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Motivo.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
