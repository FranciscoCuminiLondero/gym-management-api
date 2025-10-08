using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class GymDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Plan> Planes { get; set; }
    public DbSet<Membresia> Membresias { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Clase> Clases { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<Auditoria> Auditorias { get; set; }

    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicar configuraciones usando métodos de extensión
        modelBuilder.ConfigureUsuarios();
        modelBuilder.ConfigureAlumno();
        modelBuilder.ConfigureProfesor();
        modelBuilder.ConfigureOtrasEntidades();

        // Seed data para roles
        modelBuilder.Entity<Rol>().HasData(
            new Rol { Id = 1, Nombre = "Administrador" },
            new Rol { Id = 2, Nombre = "Alumno" },
            new Rol { Id = 3, Nombre = "Profesor" }
        );

        base.OnModelCreating(modelBuilder);
    }
}