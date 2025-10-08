namespace Domain.Entities
{
    public class Profesor : Usuario
    {
        public Profesor()
        {
            Role = "Profesor";
        }

        public List<Clase> Clases { get; set; }
    }
}
