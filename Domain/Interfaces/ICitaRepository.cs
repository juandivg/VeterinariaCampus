using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
    public interface ICitaRepository:IGenericRepository<Cita>
    {
        Task<IEnumerable<Cita>> GetMascotasCita(string motivo, DateTime fechainicio, DateTime fechafinal);
        Task<IEnumerable<Cita>> GetMascotasVeterinario(string veterinario);
    }
