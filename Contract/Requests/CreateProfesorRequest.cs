namespace Contract.Requests
{
    public class CreateProfesorRequest
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Dni { get; set; }
        public required string Email { get; set; }
        public required string Telefono { get; set; }
    }
}
