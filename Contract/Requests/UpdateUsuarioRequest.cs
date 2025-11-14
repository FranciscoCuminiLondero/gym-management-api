namespace Contract.Requests
{
    public class UpdateUsuarioRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Genero { get; set; }
        public string? Direccion { get; set; }
        public string? Image { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public int? SucursalId { get; set; }
    }
}
