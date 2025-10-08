namespace Domain.Entities
{
    public class Reserva : BaseEntity
    {
        public int AlumnoId { get; set; }
        public int ClaseId { get; set; }
        public DateOnly FechaReserva { get; set; }
        public bool Activo { get; set; } = true;

        public Alumno Alumno { get; set; } = null!;
        public Clase Clase { get; set; } = null!;
    }
}
