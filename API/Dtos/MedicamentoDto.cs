using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class MedicamentoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int IdLaboratoriofk { get; set; }
    }
