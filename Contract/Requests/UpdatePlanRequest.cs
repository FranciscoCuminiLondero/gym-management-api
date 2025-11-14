namespace Contract.Requests
{
    public class UpdatePlanRequest
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? DuracionDias { get; set; }
        public int? MaxReservasPorMes { get; set; }
        public List<string>? TiposPermitidos { get; set; }
    }
}
