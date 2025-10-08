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
        public DbSet<Usuario> Usuarios { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table-per-hierarchy (TPH) mapping for Usuario, Alumno and Profesor
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Usuario>("Usuario")
                .HasValue<Alumno>("Alumno")
                .HasValue<Profesor>("Profesor");

            var adminPassword = "Admin123!";
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(adminPassword));
                var adminHash = System.Convert.ToBase64String(hashedBytes);

                modelBuilder.Entity<Usuario>().HasData(new Usuario
                {
                    Id = -9999,
                    Nombre = "Admin",
                    Apellido = "User",
                    Dni = "00000000",
                    Email = "admin@gym.com",
                    Telefono = "",
                    FechaNacimiento = DateOnly.FromDateTime(new System.DateTime(1990, 1, 1)),
                    Activo = true,
                    PasswordHash = adminHash,
                    Role = "Administrador"
                });
            }
        }

    }
}
