namespace Application.Services
{
    public interface IPagoService
    {
        void RegistrarPagoInicial(int membresiaId, decimal monto);
    }
}
