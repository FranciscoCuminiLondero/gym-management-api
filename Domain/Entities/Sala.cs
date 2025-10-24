namespace Domain.Entities
{
    public class Sala : BaseEntity
    {
        public int SucursalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Activa { get; set; } = true;

        public Sucursal? Sucursal { get; set; }
        public List<Clase>? Clases { get; set; }
    }
}
