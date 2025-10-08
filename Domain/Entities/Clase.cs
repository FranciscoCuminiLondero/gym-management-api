using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Clase : BaseEntity
    {
        public int ProfesorId { get; set; }
        public int SalaId { get; set; }
        public int SucursalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int DuracionMinutos { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public DateOnly Fecha { get; set; }
        public int Capacidad { get; set; }
        public bool Activa { get; set; } = true;

        public Profesor Profesor { get; set; } = null!;
        public Sala Sala { get; set; } = null!;
        public Sucursal Sucursal { get; set; } = null!;
        public List<Reserva> Reservas { get; set; } = new();
    }
}
