namespace Contract.Requests
{
    public class UpdateClaseRequest
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public string? Tipo { get; set; }
        public int? DuracionMinutos { get; set; }
        public TimeOnly? HoraInicio { get; set; }
        public DateOnly? Fecha { get; set; }
        public List<string>? Dias { get; set; }
        public int? Capacidad { get; set; }
        public bool? MostrarEnHome { get; set; }
    }
}
