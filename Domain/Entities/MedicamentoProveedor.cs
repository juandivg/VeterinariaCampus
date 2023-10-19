using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedicamentoProveedor
    {
        public int IdProveedorfk { get; set; }
        public Proveedor Proveedor { get; set; }
        public int IdMedicamentofk { get; set; }
        public Medicamento Medicamento { get; set; }
    }
}