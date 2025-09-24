using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Clase
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public int SalaId { get; set; }
        public int SucursalId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public DateOnly Fecha {  get; set; }
        public int Capacidad { get; set; }
        public bool Activa { get; set; }

        public Profesor Profesor { get; set; }
        public Sala Sala { get; set; }
        public Sucursal Sucursal { get; set; }
        public List<Reserva> Reservas { get; set; }
    }
}
