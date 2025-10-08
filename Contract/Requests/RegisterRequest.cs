namespace Contract.Requests
{
    public class RegisterRequest : BaseUsuarioRequest
    {
        public string Role { get; set; } = "Alumno";
        public int? PlanId { get; set; }
        public string? Especialidad { get; set; }
    }
}
