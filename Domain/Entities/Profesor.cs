namespace Domain.Entities
{
    public class Profesor : Usuario
    {
        public Profesor()
        {
            Role = "Profesor";
        }

        public string? Especialidad { get; set; }
        public List<Clase> Clases { get; set; }
    }
}
