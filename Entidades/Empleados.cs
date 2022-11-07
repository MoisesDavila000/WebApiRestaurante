using System.ComponentModel.DataAnnotations;
using WebApiRestaurante2.Validaciones;

namespace WebApiRestaurante2.Entidades
{
    public class Empleados
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombres es requerido.")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo tipo no puede exceder los 20 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo apellidos es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo tipo no puede exceder los 50 caracteres")]
        [PrimeraLetraMayuscula]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo matricula es requerido.")]
        public int Matricula { get; set; }

        public int TurnosId { get; set; }

        public Turnos Turno { get; set; }

    }
}
