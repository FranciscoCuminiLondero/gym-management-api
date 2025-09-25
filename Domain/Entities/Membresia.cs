namespace Domain.Entities
{
    public class Membresia : BaseEntity
    {
        public int AlumnoId { get; set; }
        public int PlanId {  get; set; }
        public DateOnly FechaInicio  { get; set; }
        public DateOnly FechaFin {  get; set; }
        public bool Activa { get; set; }
        public Alumno Alumno { get; set; }
        public Plan Plan { get; set; }
        public List<Pago> Pagos { get; set; }
    }
}
