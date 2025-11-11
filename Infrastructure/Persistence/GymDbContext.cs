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

            // Seed data para Sucursales
            modelBuilder.Entity<Sucursal>().HasData(
                new Sucursal
                {
                    Id = 1,
                    Nombre = "Sucursal Centro",
                    Direccion = "Av. Principal 123",
                    Telefono = "555-0001",
                    Email = "centro@gym.com",
                    Activa = true
                },
                new Sucursal
                {
                    Id = 2,
                    Nombre = "Sucursal Norte",
                    Direccion = "Calle Norte 456",
                    Telefono = "555-0002",
                    Email = "norte@gym.com",
                    Activa = true
                }
            );

            // Seed data para Salas
            modelBuilder.Entity<Sala>().HasData(
                new Sala
                {
                    Id = 1,
                    Nombre = "Sala A",
                    Tipo = "Multiuso",
                    Capacidad = 25,
                    Descripcion = "Sala multiuso para yoga, pilates y actividades grupales",
                    SucursalId = 1,
                    Activa = true
                },
                new Sala
                {
                    Id = 2,
                    Nombre = "Sala B",
                    Tipo = "Spinning",
                    Capacidad = 20,
                    Descripcion = "Sala equipada con 20 bicicletas estáticas profesionales",
                    SucursalId = 1,
                    Activa = true
                },
                new Sala
                {
                    Id = 3,
                    Nombre = "Sala 1",
                    Tipo = "Funcional",
                    Capacidad = 30,
                    Descripcion = "Espacio amplio para entrenamiento funcional y crossfit",
                    SucursalId = 2,
                    Activa = true
                },
                new Sala
                {
                    Id = 4,
                    Nombre = "Sala 2",
                    Tipo = "Pesas",
                    Capacidad = 40,
                    Descripcion = "Sala de musculación con equipamiento completo",
                    SucursalId = 2,
                    Activa = true
                }
            );
        }

    }
}
