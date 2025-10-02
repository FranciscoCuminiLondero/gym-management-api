namespace Contract.Responses
{
    public class PlanResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int DuracionDias { get; set; }
    }
}
