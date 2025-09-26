namespace Domain.Entities
{
    public class Profesor : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "Profesor";
        public string Dni {  get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public List<Clase> Clases { get; set; }
    }
}
