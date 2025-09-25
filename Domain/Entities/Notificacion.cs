namespace Domain.Entities
{
    public class Notificacion : BaseEntity
    {
        public string Tipo { get; set; }
        public string Destino { get; set; }
        public string Mensaje { get; set; }
        public DateOnly FechaEnvio { get; set; }
        public bool Enviado { get; set; }
    }
}
