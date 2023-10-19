using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class MovimientoMedicamento:BaseEntity
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int IdUsuariofk { get; set; }
        public Usuario Usuario { get; set; }
        public int IdPropetiariofk { get; set; }
        public Propietario Propietario { get; set; }
        public int IdTipoMovimientofk { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public int IdMedicamentofk { get; set; }
        public Medicamento Medicamento { get; set; }
    }
