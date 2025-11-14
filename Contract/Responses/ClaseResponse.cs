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
        public string? Imagen { get; set; }
        public int CupoMaximo { get; set; } // Era Capacidad
        public string? Tipo { get; set; } // "general" o "especializada"
        public string HorarioInicio { get; set; } = string.Empty; // ISO String combinado de Fecha + HoraInicio
        public string HorarioFin { get; set; } = string.Empty; // ISO String calculado
        public List<string> Dias { get; set; } = new List<string>(); // ["Lunes", "Miércoles"]
        public bool MostrarEnHome { get; set; }
        public int CuposActuales { get; set; } // Calculado: Capacidad - Reservas
        public int CupoDisponible { get; set; } // Reservas disponibles
        public bool Activa { get; set; }

        // Campos heredados del sistema anterior (para compatibilidad)
        public int Duracion { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public DateOnly Fecha { get; set; }
        public int Capacidad { get; set; }
    }
}
