using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Medicamento:BaseEntity
    {
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int  IdLaboratoriofk{ get; set; }
        public Laboratorio Laboratorio { get; set; }
        public ICollection<MedicamentoProveedor> MedicamentoProveedores { get; set; }
        public ICollection<Proveedor> Proveedores = new HashSet<Proveedor>();
        public ICollection<MovimientoMedicamento> MovimientoMedicamentos { get; set; }
        public ICollection<Tratamiento> Tratamientos { get; set; }
        
    }
