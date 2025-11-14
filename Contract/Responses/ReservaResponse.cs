namespace Contract.Responses
{
    public class ReservaResponse
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int ClaseId { get; set; }
        public string FechaReserva { get; set; } = string.Empty; // ISO String
        public string Estado { get; set; } = "confirmada"; // "confirmada", "cancelada", "pendiente"
        public string CreatedAt { get; set; } = string.Empty; // ISO String
        public bool Activo { get; set; } // Mantener para compatibilidad
    }
}
