using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }

        public Sucursal Sucursal { get; set; }
        public List<Clase> Clases { get; set; }
    }
}
