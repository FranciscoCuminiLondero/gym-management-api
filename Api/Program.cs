using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.Zero // Eliminar el tiempo de gracia por defecto
        };
    });

// Configuración de autorización con políticas específicas
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AlumnoPolicy", policy =>
        policy.RequireRole("Alumno"));

    options.AddPolicy("ProfesorPolicy", policy =>
        policy.RequireRole("Profesor"));

    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("SuperAdmin", "Administrador"));

    options.AddPolicy("AlumnoOrProfesorPolicy", policy =>
        policy.RequireRole("Alumno", "Profesor"));
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("GymManagementPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3001") // Agregar orígenes del frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configuración para manejo de fechas y enums
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener PascalCase
        options.JsonSerializerOptions.WriteIndented = true; // JSON formateado
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Gym Management API", Version = "v1" });

    // Configuración de JWT en Swagger
    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new() { Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Configuración de Infrastructure y servicios usando el método de extensión
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

// Middleware global de manejo de excepciones
app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

// Importante: CORS debe ir antes de Authentication y Authorization
app.UseCors("GymManagementPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
