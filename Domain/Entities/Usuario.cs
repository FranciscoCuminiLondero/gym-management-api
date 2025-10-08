namespace Domain.Entities
{
    public abstract class Usuario : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public int RolId { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public virtual string GetTipoUsuario() => "Usuario";
    }
}
