namespace Contract.Responses
{
    public class AlumnoResponse : BaseUsuarioResponse
    {
        public int PlanId { get; set; }
        public string PlanNombre { get; set; } = string.Empty;
        public bool TieneMembresiaActiva { get; set; }
        public List<MembresiaResponse> Membresias { get; set; } = new();
        public List<ReservaResponse> ReservasActivas { get; set; } = new();
    }
}
