using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public static class EntityConfigurations
    {
        public static void ConfigureUsuarios(this ModelBuilder modelBuilder)
        {
            // Configuración de herencia TPH (Table Per Hierarchy)
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Alumno>("Alumno")
                .HasValue<Profesor>("Profesor");

            // Índices para mejorar rendimiento
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Dni)
                .IsUnique();
        }

        public static void ConfigureAlumno(this ModelBuilder modelBuilder)
        {
            // Relación Alumno-Plan
            modelBuilder.Entity<Alumno>()
                .HasOne(a => a.Plan)
                .WithMany()
                .HasForeignKey(a => a.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Alumno-Membresías
            modelBuilder.Entity<Alumno>()
                .HasMany(a => a.Membresias)
                .WithOne(m => m.Alumno)
                .HasForeignKey(m => m.AlumnoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Alumno-Reservas
            modelBuilder.Entity<Alumno>()
                .HasMany(a => a.Reservas)
                .WithOne(r => r.Alumno)
                .HasForeignKey(r => r.AlumnoId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public static void ConfigureProfesor(this ModelBuilder modelBuilder)
        {
            // Relación Profesor-Clases
            modelBuilder.Entity<Profesor>()
                .HasMany(p => p.Clases)
                .WithOne(c => c.Profesor)
                .HasForeignKey(c => c.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de especialidad
            modelBuilder.Entity<Profesor>()
                .Property(p => p.Especialidad)
                .HasMaxLength(100);
        }

        public static void ConfigureOtrasEntidades(this ModelBuilder modelBuilder)
        {
            // Configuración de Membresía
            modelBuilder.Entity<Membresia>()
                .HasOne(m => m.Plan)
                .WithMany()
                .HasForeignKey(m => m.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Clase
            modelBuilder.Entity<Clase>()
                .HasOne(c => c.Sala)
                .WithMany()
                .HasForeignKey(c => c.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Clase>()
                .HasOne(c => c.Sucursal)
                .WithMany()
                .HasForeignKey(c => c.SucursalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Clase)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices para mejorar rendimiento
            modelBuilder.Entity<Clase>()
                .HasIndex(c => new { c.Fecha, c.HoraInicio, c.SalaId });

            modelBuilder.Entity<Reserva>()
                .HasIndex(r => new { r.FechaReserva, r.AlumnoId });

            modelBuilder.Entity<Membresia>()
                .HasIndex(m => new { m.FechaFin, m.Activa, m.AlumnoId });
        }
    }
}