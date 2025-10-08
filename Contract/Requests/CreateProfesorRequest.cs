namespace Contract.Requests
{
    public class CreateProfesorRequest : BaseUsuarioRequest
    {
        public string? Especialidad { get; set; }
        public DateTime? FechaContratacion { get; set; }
    }
}
