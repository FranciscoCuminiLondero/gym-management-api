namespace Contract.Responses
{
    public class SalaResponse
    {
        public int Id { get; set; }
        public int SucursalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Activa { get; set; }
    }
}
