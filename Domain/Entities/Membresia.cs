using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Membresia
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public string PlanId {  get; set; }
        public DateOnly FechaInicio  { get; set; }
        public DateOnly FechaFin {  get; set; }
        public bool Activa { get; set; }
        public Alumno Alumno { get; set; }
        public Plan Plan { get; set; }
        public List<Pago> Pagos { get; set; }
    }
}
