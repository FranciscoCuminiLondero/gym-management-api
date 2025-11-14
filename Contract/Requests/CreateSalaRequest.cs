namespace Contract.Requests
{
    public class CreateSalaRequest
    {
        public int SucursalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
