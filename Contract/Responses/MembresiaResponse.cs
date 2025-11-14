namespace Contract.Responses
{
    public class MembresiaResponse
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int AlumnoId { get; set; }
        public string FechaInicio { get; set; } = string.Empty; // ISO String
        public string FechaFin { get; set; } = string.Empty; // ISO String
        public string Estado { get; set; } = "activa"; // "activa", "inactiva", "expirada"
        public bool Activa { get; set; } // Mantener para compatibilidad
    }
}