using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Views;
    public class SpeciesxPets
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Mascota> Mascotas { get; set; }
    }
