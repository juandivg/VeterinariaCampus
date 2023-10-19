using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;
  
        public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly VeterinariaCampusContext _context;
        private IMascotaRepository _mascotas;
        private IProveedorRepository _proveedores;
        private IPropietarioRepository _propietarios;
        private IMedicamentoRepository _medicamentos;
        private IUsuarioRepository _usuarios;
        private IRolRepository _roles;
        private IVeterinarioRepository _veterinarios;
        private IMovimientoMedicamentoRepository _movimientomedicamentos;
        private ICitaRepository _citas;
        private IEspecieRepository _especies;
        private IRazaRepository _razas;

        public UnitOfWork(VeterinariaCampusContext context)
        {
            _context= context;
        }
        public IMascotaRepository Mascotas
        {
            get{
                if(_mascotas==null)
                {
                    _mascotas=new MascotaRepository(_context);
                }
                return _mascotas;
            }
        }
        public IPropietarioRepository Propietarios
        {
            get{
                if(_propietarios==null)
                {
                    _propietarios=new PropietarioRepository(_context);
                }
                return _propietarios;
            }
        }
        public IProveedorRepository Proveedores
        {
            get{
                if(_proveedores==null)
                {
                    _proveedores=new ProveedorRepository(_context);
                }
                return _proveedores;
            }
        }
        public IMedicamentoRepository Medicamentos
        {
            get{
                if(_medicamentos==null)
                {
                    _medicamentos=new MedicamentoRepository(_context);
                }
                return _medicamentos;
            }
        }
        public IVeterinarioRepository Veterinarios
        {
            get{
                if(_veterinarios==null)
                {
                    _veterinarios=new VeterinarioRepository(_context);
                }
                return _veterinarios;
            }
        }
        public IUsuarioRepository Usuarios
        {
            get{
                if(_usuarios==null)
                {
                    _usuarios=new UsuarioRepository(_context);
                }
                return _usuarios;
            }
        }
        public IRolRepository Roles
        {
            get{
                if(_roles==null)
                {
                    _roles=new RolRepository(_context);
                }
                return _roles;
            }
        }
        public IMovimientoMedicamentoRepository MovimientoMedicamentos
        {
            get{
                if(_movimientomedicamentos==null)
                {
                    _movimientomedicamentos=new MovimientoMedicamentoRepository(_context);
                }
                return _movimientomedicamentos;
            }
        }
        public ICitaRepository Citas
        {
            get{
                if(_citas==null)
                {
                    _citas=new CitaRepository(_context);
                }
                return _citas;
            }
        }
        public IEspecieRepository Especies
        {
            get{
                if(_especies==null)
                {
                    _especies=new EspecieRepository(_context);
                }
                return _especies;
            }
        }
        public IRazaRepository Razas
        {
            get{
                if(_razas==null)
                {
                    _razas=new RazaRepository(_context);
                }
                return _razas;
            }
        }
    
        
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
