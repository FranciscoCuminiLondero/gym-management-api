namespace Domain.Entities
{
    public class Reserva : BaseEntity
    {
        public int AlumnoId { get; set; }
        public int ClaseId { get; set; }
        public DateOnly FechaReserva {  get; set; }
        public bool Activo {  get; set; }

        public Alumno Alumno {  get; set; }
        public Clase Clase { get; set; }
    }
}
