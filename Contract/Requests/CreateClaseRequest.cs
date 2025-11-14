namespace Contract.Requests
{
    public class CreateClaseRequest
    {
        public int ProfesorId { get; set; }
        public int SalaId { get; set; }
        public int SucursalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Imagen { get; set; }
        public string? Tipo { get; set; } // "general" o "especializada"
        public int DuracionMinutos { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public DateOnly Fecha { get; set; }
        public List<string>? Dias { get; set; } // ["Lunes", "Miércoles"]
        public int Capacidad { get; set; }
        public bool MostrarEnHome { get; set; } = true;
    }
}
