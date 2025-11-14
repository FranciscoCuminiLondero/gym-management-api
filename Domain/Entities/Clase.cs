using System;
using System.Collections.Generic;
namespace Domain.Entities
{
    public class Clase : BaseEntity
    {
        public int ProfesorId { get; set; }
        public int SalaId { get; set; }
        public int SucursalId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Imagen { get; set; }
        public string? Tipo { get; set; } // "general" o "especializada"
        public int DuracionMinutos { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Dias { get; set; } // JSON string: ["Lunes", "Miércoles"]
        public int Capacidad { get; set; }
        public bool MostrarEnHome { get; set; } = true;
        public bool Activa { get; set; }

        public Profesor Profesor { get; set; }
        public Sala Sala { get; set; }
        public Sucursal Sucursal { get; set; }
        public List<Reserva> Reservas { get; set; }
    }
}
