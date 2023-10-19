using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class MovimientoTotalDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string NombrePropietario { get; set; }

        public string NombreMedicamento  { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
