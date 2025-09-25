namespace Domain.Entities
{
    public class Sala : BaseEntity
    {
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }

        public Sucursal Sucursal { get; set; }
        public List<Clase> Clases { get; set; }
    }
}
