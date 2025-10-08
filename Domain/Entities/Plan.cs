namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int DuracionDias { get; set; }
        public bool Activo { get; set; } = true;
        public List<Membresia> Membresias { get; set; } = new();
    }
}
