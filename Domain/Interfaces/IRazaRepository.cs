using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
    public interface IRazaRepository:IGenericRepository<Raza>
    {
        Task<IEnumerable<Raza>> GetRazasxMascotas();
    }
