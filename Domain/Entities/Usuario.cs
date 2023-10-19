using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Usuario: BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
        public ICollection<Rol> Roles = new HashSet<Rol>();
        public ICollection<RefreshToken> RefreshTokens = new HashSet<RefreshToken>();
        public ICollection<MovimientoMedicamento> MovimientoMedicamentos { get; set; }
    }
