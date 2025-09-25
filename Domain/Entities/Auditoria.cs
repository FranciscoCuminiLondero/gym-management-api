namespace Domain.Entities
{
    public class Auditoria : BaseEntity
    {
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
    }
}
