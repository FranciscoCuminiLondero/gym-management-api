using Application.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public void RegistrarPagoInicial(int membresiaId, decimal monto)
        {
            var pago = new Pago
            {
                MembresiaId = membresiaId,
                Monto = monto,
                FechaPago = DateOnly.FromDateTime(DateTime.Today),
                MetodoPago = "Sistema",
                Estado = true,
            };

            _pagoRepository.Create(pago);
        }
    }
}
