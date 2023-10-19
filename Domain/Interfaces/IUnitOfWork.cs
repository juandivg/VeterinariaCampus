using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;
    public interface IUnitOfWork
    {
        IMascotaRepository Mascotas {get;}
        IMedicamentoRepository Medicamentos {get;}
        IProveedorRepository Proveedores {get;}
        IMovimientoMedicamentoRepository MovimientoMedicamentos {get;}
        IUsuarioRepository Usuarios    {get;}
        IRolRepository Roles {get;}
        IPropietarioRepository Propietarios {get;}
        IVeterinarioRepository Veterinarios {get;}
        ICitaRepository Citas {get;}
        IEspecieRepository Especies {get;}
        IRazaRepository Razas {get;}
        Task<int> SaveAsync();
    }
