using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos;
    public class CitaxMascotaDto
    {
        public int Id { get; set; }
        public MascotaDto Mascota { get; set; }
    }
