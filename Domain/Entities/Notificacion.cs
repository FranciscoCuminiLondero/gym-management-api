using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notificacion
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Destino { get; set; }
        public string Mensaje { get; set; }
        public DateOnly FechaEnvio { get; set; }
        public bool Enviado { get; set; }
    }
}
