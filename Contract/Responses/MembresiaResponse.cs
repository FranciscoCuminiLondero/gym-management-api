namespace Contract.Responses
{
    public class MembresiaResponse
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public bool Activa { get; set; }
    }
}