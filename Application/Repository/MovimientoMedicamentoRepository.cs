using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application.Repository;
public class MovimientoMedicamentoRepository : GenericRepository<MovimientoMedicamento>, IMovimientoMedicamentoRepository
{
    private readonly VeterinariaCampusContext _context;
    public MovimientoMedicamentoRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }


    public async Task<IEnumerable<MovimientoTotal>> GetMovimientos()
    {
        return await(
            from mov in _context.MovimientoMedicamentos
            join p in _context.Propietarios on mov.IdPropetiariofk equals p.Id
            join med in _context.Medicamentos on mov.IdMedicamentofk equals med.Id
            select new MovimientoTotal
            {
                Id=mov.Id,
                Fecha=mov.Fecha,
                NombrePropietario=p.Nombre,
                NombreMedicamento=med.Nombre,
                Cantidad=mov.Cantidad,
                Total=mov.Cantidad*med.Precio

            }

        ).ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<MovimientoMedicamento> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.MovimientoMedicamentos as IQueryable<MovimientoMedicamento>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Medicamento.Nombre.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
