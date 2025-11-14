namespace Contract.Responses
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Lastname { get; set; } // Apellido
        public string Email { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; } // Convertido desde Role
        public string TelNumber { get; set; } // Telefono
        public string Dni { get; set; }
        public string? Genero { get; set; }
        public string FechaNacimiento { get; set; } // Formato YYYY-MM-DD
        public string? Direccion { get; set; }
        public string Estado { get; set; } // "activo" o "inactivo"
        public string? Plan { get; set; } // Nombre del plan activo
        public int? SucursalId { get; set; }
        public string? Image { get; set; }
    }
}
