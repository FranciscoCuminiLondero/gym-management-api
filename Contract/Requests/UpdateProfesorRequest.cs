namespace Contract.Requests
{
    public class UpdateProfesorRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
    }
}
