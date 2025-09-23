using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pago
    {
        public int Id { get; set; }
        public int MembresiaId { get; set; }
        public decimal Monto { get; set; }
        public DateOnly FechaPago { get; set; }
        public DateOnly FechaVencimiento { get; set; }
        public string MetodoPago { get; set; }
        public bool Estado { get; set; }
        public Membresia Membresia { get; set; }
    }
}
