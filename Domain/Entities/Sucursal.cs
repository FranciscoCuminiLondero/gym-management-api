namespace Domain.Entities
{
    public class Sucursal : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activa { get; set; } = true;

        public List<Sala> Salas { get; set; } = new();
        public List<Clase> Clases { get; set; } = new();
    }
}
