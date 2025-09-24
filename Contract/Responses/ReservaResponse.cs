namespace Contract.Responses
{
    public class ReservaResponse
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int ClaseId { get; set; }
        public DateOnly FechaReserva { get; set; }
        public bool Activo { get; set; }
    }
}
