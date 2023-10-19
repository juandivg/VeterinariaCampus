using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Proveedor:BaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public ICollection<MedicamentoProveedor> MedicamentoProveedores { get; set; }
        public ICollection<Medicamento> Medicamentos = new HashSet<Medicamento>();
    }
