using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly VeterinariaCampusContext _context;
    public RolRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }
}
