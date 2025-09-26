namespace Contract.Responses
{
    public class ClaseResponse
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public int SalaId { get; set; }
        public int SucursalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Duracion { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public DateOnly Fecha { get; set; }
        public int Capacidad { get; set; }
        public int CupoDisponible { get; set; }
        public bool Activa { get; set; }
    }
}
