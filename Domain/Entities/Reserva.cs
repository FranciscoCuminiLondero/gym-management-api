using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int ClaseId { get; set; }
        public DateOnly FechaReserva {  get; set; }
        public bool Activo {  get; set; }

        public Alumno Alumno {  get; set; }
        public Clase Clase { get; set; }
    }
}
