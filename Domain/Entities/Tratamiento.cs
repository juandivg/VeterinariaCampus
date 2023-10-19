using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Tratamiento:BaseEntity
    {
        public decimal Dosis { get; set; }
        public DateTime FechaAdministracion { get; set; }
        public string Observacion { get; set; }
        public int IdCitafk { get; set; }
        public Cita Cita { get; set; }
        public int IdMedicamentofk { get; set; }
        public Medicamento Medicamento { get; set; }
    }
