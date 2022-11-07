using System.ComponentModel.DataAnnotations;
using WebApiRestaurante2.Validaciones;

namespace WebApiRestaurante2.Entidades
{
    public class Turnos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Tipo es requerido.")]
        [StringLength(maximumLength: 2, ErrorMessage = "El campo tipo no puede exceder los 2 caracteres, TC = Turno Completo TM = Turno Medio")]
        [PrimeraLetraMayuscula]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El campo entrada es requerido.")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo entrada no puede exceder los 5 caracteres, formato de 24h")]
        [PrimeraLetraMayuscula]
        public string Entrada { get; set; }

        [Required(ErrorMessage = "El campo salida es requerido.")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo salida no puede exceder los 5 caracteres, formato de 24h")]
        [PrimeraLetraMayuscula]
        public string Salida { get; set; }

        public List<Empleados> Empleados { get; set; }
    }
}
