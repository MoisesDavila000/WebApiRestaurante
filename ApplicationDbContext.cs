using Microsoft.EntityFrameworkCore;
using WebApiRestaurante.Entidades;

namespace WebApiRestaurante
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Platillos> Platillos { get; set; }

        public DbSet<Acompañamiento> Acompañamientos { get; set; }
    }
}
