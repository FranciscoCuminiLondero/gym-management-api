namespace Contract.Responses
{
    public class AlumnoPerfilResponse
    {
        public AlumnoResponse Alumno { get; set; }
        public MembresiaResponse? MembresiaActiva { get; set; }
        public List<ReservaResponse> Reservas { get; set; }
    }
}
