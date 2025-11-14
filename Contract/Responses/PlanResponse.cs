namespace Contract.Responses
{
    public class PlanResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int DuracionMeses { get; set; } // Calculado desde DuracionDias
        public string Estado { get; set; } // "activo" o "inactivo"
        public int? MaxReservasPorMes { get; set; }
        public List<string> TiposPermitidos { get; set; } = new List<string>(); // ["general", "especializada"]
    }
}
