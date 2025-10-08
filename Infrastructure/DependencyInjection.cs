using Application.Abstractions;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuración de Entity Framework
            services.AddDbContext<GymDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<IAlumnoRepository, AlumnoRepository>();
            services.AddScoped<IProfesorRepository, ProfesorRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IMembresiaRepository, MembresiaRepository>();
            services.AddScoped<IClaseRepository, ClaseRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();

            // Servicios de aplicación
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAlumnoService, AlumnoService>();
            services.AddScoped<IProfesorService, ProfesorService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IMembresiaService, MembresiaService>();
            services.AddScoped<IClaseService, ClaseService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<INotificacionService, NotificacionService>();
            services.AddScoped<IPagoService, PagoService>();

            return services;
        }
    }
}