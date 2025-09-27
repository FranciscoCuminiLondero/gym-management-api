using Application.Abstractions;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Detectar si estamos en Render (usa una variable de entorno que Render siempre define)
var isRender = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RENDER"));

string connectionString;
if (isRender)
{
    // Render: usa PostgreSQL
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (string.IsNullOrEmpty(databaseUrl))
        throw new InvalidOperationException("DATABASE_URL is required in Render environment.");
    connectionString = databaseUrl;
    builder.Services.AddDbContext<GymDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // Local: usa SQLite
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
