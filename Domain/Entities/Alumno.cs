namespace Domain.Entities
{
    public class Alumno : Usuario
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!; // Navigation property
        public List<Membresia> Membresias { get; set; } = new();
        public List<Reserva> Reservas { get; set; } = new();

        public Alumno()
        {
            RolId = (int)TipoRol.Alumno;
        }

        public override string GetTipoUsuario() => "Alumno";

        // Método para verificar si tiene membresía activa
        public bool TieneMembresiaActiva()
        {
            return Membresias.Any(m => m.FechaFin > DateOnly.FromDateTime(DateTime.Now) && m.Activa);
        }
    }
}
