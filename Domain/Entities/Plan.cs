namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int DuracionDias { get; set; }
        public bool Activo {  get; set; }
        public List<Membresia> Membresias { get; set; }
    }
}
