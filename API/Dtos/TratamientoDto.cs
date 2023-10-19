using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class TratamientoDto
    {
        public int Id { get; set; }
        public decimal Dosis { get; set; }
        public DateTime FechaAdministracion { get; set; }
        public string Observacion { get; set; }
        public int IdCitafk { get; set; }
        public int IdMedicamentofk { get; set; }
    }
