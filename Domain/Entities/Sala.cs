namespace Domain.Entities
{
    public class Sala : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int SucursalId { get; set; }

        public Sucursal Sucursal { get; set; } = null!;
        public List<Clase> Clases { get; set; } = new();
    }
}
