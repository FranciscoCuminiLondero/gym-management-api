using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
    }
}
