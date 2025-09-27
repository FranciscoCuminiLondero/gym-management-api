using Application.Abstractions;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Detectar si estamos en Render
var isRender = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RENDER"));

string connectionString;
if (isRender)
{
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (string.IsNullOrEmpty(databaseUrl))
        throw new InvalidOperationException("DATABASE_URL is required in Render environment.");
    connectionString = databaseUrl;
    builder.Services.AddDbContext<GymDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    connectionString = "Data Source=gym.db";
    builder.Services.AddDbContext<GymDbContext>(options =>
        options.UseSqlite(connectionString));
}

// Registro de servicios y repositorios
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IClaseService, ClaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IClaseRepository, ClaseRepository>();

var app = builder.Build();

// Aplicar migraciones en producción
if (!app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("ENABLE_SWAGGER") == "true")
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym Management API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();