using Microsoft.EntityFrameworkCore;
using WebApiRestaurante2.Entidades;

namespace WebApiRestaurante2
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Platillos> Platillos { get; set; }

        public DbSet<Acompañamiento> Acompañamientos { get; set; }

        public DbSet<Empleados> Empleados { get; set; }

        public DbSet<Turnos> Turnos { get; set; }
    }
}
