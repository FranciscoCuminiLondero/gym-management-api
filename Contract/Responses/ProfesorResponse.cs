namespace Contract.Responses
{
    public class ProfesorResponse : BaseUsuarioResponse
    {
        public string? Especialidad { get; set; }
        public DateTime? FechaContratacion { get; set; }
        public List<ClaseResponse> ClasesActivas { get; set; } = new();
        public int TotalClasesAsignadas { get; set; }
    }
}
