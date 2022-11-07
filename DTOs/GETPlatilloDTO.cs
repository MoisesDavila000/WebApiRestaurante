using WebApiRestaurante2.Entidades;

namespace WebApiRestaurante2.DTOs
{
    public class GETPlatilloDTO
    {

        public int Id { get; set; }

        public string Tipo { get; set; }

        public string Nombre { get; set; }

        public int Precio { get; set; }

        public string Descricpion { get; set; }

    }
}