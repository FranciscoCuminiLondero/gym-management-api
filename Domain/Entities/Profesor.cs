namespace Domain.Entities
{
    public class Profesor : Usuario
    {
        public List<Clase> Clases { get; set; } = new();
        public string? Especialidad { get; set; }
        public DateTime? FechaContratacion { get; set; }

        public Profesor()
        {
            RolId = (int)TipoRol.Profesor;
        }

        public override string GetTipoUsuario() => "Profesor";

        // Método para obtener clases activas
        public List<Clase> GetClasesActivas()
        {
            var fechaActual = DateOnly.FromDateTime(DateTime.Now);
            var horaActual = TimeOnly.FromDateTime(DateTime.Now);

            return Clases.Where(c => c.Activa &&
                (c.Fecha > fechaActual ||
                (c.Fecha == fechaActual && c.HoraInicio > horaActual))).ToList();
        }
    }
}
