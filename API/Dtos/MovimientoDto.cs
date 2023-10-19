using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class MovimientoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int IdUsuariofk { get; set; }

        public int IdPropetiariofk { get; set; }
        
        public int IdTipoMovimientofk { get; set; }
        
        public int IdMedicamentofk { get; set; }
    }
