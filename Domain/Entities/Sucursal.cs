namespace Domain.Entities
{
    public class Sucursal : BaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Activa {  get; set; }

        public List<Sala> Salas { get; set; }
        public List<Clase> Clases { get; set; }

    }
}
