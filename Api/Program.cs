using Application.Abstractions;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isRender = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RENDER"));

string connectionString;
if (isRender)
{
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (string.IsNullOrEmpty(databaseUrl))
        throw new InvalidOperationException("DATABASE_URL is required in Render environment.");

    var databaseUri = new Uri(databaseUrl);
    var userInfo = databaseUri.UserInfo.Split(':');
    var port = databaseUri.Port == -1 ? 5432 : databaseUri.Port;

    connectionString = new NpgsqlConnectionStringBuilder
    {
        Host = databaseUri.Host,
        Port = port,
        Database = databaseUri.LocalPath.TrimStart('/'),
        Username = userInfo[0],
        Password = userInfo[1],
        SslMode = SslMode.Require
        // TrustServerCertificate = true // opcional, pero no necesario en Render
    }.ToString();
}
else
{
    connectionString = "Data Source=gym.db";
    builder.Services.AddDbContext<GymDbContext>(options =>
        options.UseSqlite(connectionString));
}

// Registro de DbContext
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registro de servicios y repositorios
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IClaseService, ClaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IMembresiaService, MembresiaService>();

builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IClaseRepository, ClaseRepository>();    
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IMembresiaRepository, MembresiaRepository>();

var app = builder.Build();

// Aplicar migraciones en producción
if (!app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
    context.Database.Migrate();
}

// Siempre habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym Management API V1");
    c.RoutePrefix = string.Empty; // Para que Swagger quede en la raíz "/"
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();