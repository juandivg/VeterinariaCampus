using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces;
    public interface IEspecieRepository:IGenericRepository<Especie>
    {
        Task<IEnumerable<Especie>> GetEspeciesxMascotas();
        Task<IEnumerable<SpeciesxPets>> GetEspeciexMascotas2();
    }
