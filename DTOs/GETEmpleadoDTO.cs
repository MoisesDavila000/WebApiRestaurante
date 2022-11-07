using System.ComponentModel.DataAnnotations;
using WebApiRestaurante2.Entidades;
using WebApiRestaurante2.Validaciones;

namespace WebApiRestaurante2.DTOs
{
    public class GETEmpleadoDTO
    {
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int Matricula { get; set; }

    }
}
