using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class TipoMovimientoRepository:GenericRepository<TipoMovimiento>, ITipoMovimientoRepository
    {
    private readonly VeterinariaCampusContext _context;
    public TipoMovimientoRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }
    public override async Task<(int totalRegistros, IEnumerable<TipoMovimiento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.TipoMovimientos as IQueryable<TipoMovimiento>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Descripcion.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    }
