namespace Contract.Responses
{
    public class SucursalResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Salas { get; set; } // Cantidad de salas
        public bool Activa { get; set; }
    }
}
