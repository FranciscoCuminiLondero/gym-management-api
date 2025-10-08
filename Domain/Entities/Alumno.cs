namespace Domain.Entities
{
    public class Alumno : Usuario
    {
        public Alumno()
        {
            Role = "Alumno";
        }

        public List<Membresia> Membresias { get; set; }
    }
}
