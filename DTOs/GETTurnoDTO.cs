using System.ComponentModel.DataAnnotations;
using WebApiRestaurante2.Entidades;
using WebApiRestaurante2.Validaciones;

namespace WebApiRestaurante2.DTOs
{
    public class GETTurnoDTO
    {
        public int Id { get; set; }

        public string Tipo { get; set; }

        public string Entrada { get; set; }

        public string Salida { get; set; }

    }
}
