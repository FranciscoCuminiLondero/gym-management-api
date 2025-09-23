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

        public DbSet<Alumno> Alumnos {  get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Pago> Pagos { get; set; }

    }
}
