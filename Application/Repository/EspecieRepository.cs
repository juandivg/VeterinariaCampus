using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class EspecieRepository : GenericRepository<Especie>, IEspecieRepository
{
    private readonly VeterinariaCampusContext _context;
    public EspecieRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }



    public async Task<IEnumerable<SpeciesxPets>> GetEspeciexMascotas()
    {
        return await(
            from es in _context.Especies
            join r in _context.Razas on es.Id equals r.IdEspeciefk
            join m in _context.Mascotas on r.Id equals m.IdRazafk
            group m by new {es.Id, es.Nombre} into grp
            select new SpeciesxPets
            {
                Id=grp.Key.Id,
                Nombre=grp.Key.Nombre,
                Mascotas=grp.Select(p=>
                new Mascota{
                    Id=p.Id,
                    Nombre=p.Nombre,
                    FechaNacimiento=p.FechaNacimiento,
                    IdPropetiariofk=p.IdPropetiariofk,
                    IdRazafk=p.IdRazafk
                }
                ).ToList()
            }

        ).ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<Especie> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Especies as IQueryable<Especie>;
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
