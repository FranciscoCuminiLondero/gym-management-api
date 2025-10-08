namespace Contract.Responses
{
    public abstract class BaseUsuarioResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
    }
}