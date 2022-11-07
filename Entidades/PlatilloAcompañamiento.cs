using Microsoft.EntityFrameworkCore;

namespace WebApiRestaurante2.Entidades
{
    public class PlatilloAcompañamiento
    {
        public int Id { get; set; }
        public int PlatillosId { get; set; }
        public int AcompañamientoId { get; set; }
        public int Orden { get; set; }
        public Platillos Platillos { get; set; }
        public Acompañamiento Acompañamiento { get; set; }

    }
}
