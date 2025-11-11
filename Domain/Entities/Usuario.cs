using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nombre { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "Usuario";
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; }
    }
}
