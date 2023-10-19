using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Cita:BaseEntity
    {
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public int IdMascotafk { get; set; }
        public Mascota Mascota { get; set; }
        public int IdVeterinariofk { get; set; }
        public Veterinario Veterinario { get; set; }
        public ICollection<Tratamiento> Tratamientos { get; set; }

    }
