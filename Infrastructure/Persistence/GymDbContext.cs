using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

    }
}
