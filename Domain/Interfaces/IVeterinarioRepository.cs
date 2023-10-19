using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
    public interface IVeterinarioRepository:IGenericRepository<Veterinario>
    {
        Task<IEnumerable<Veterinario>> GetVeterinariosEspecialidad(string especialidad);
    }
