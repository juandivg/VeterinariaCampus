using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class CitaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public int IdMascotafk { get; set; }
        public int IdVeterinariofk { get; set; }
    }
