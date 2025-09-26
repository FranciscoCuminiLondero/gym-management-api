namespace Domain.Entities
{
    public class Alumno : BaseEntity
    {
        public string Nombre { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "Alumno";
        public string Apellido { get; set; }
        public string Dni {  get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public bool Activo {  get; set; }
        public List<Membresia> Membresias { get; set; }
    }
}
