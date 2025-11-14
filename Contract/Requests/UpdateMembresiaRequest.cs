namespace Contract.Requests
{
    public class UpdateMembresiaRequest
    {
        public int? PlanId { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public bool? Activa { get; set; }
    }
}
