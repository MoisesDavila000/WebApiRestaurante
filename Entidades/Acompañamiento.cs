using System.ComponentModel.DataAnnotations;
using WebApiRestaurante2.Validaciones;

namespace WebApiRestaurante2.Entidades
{
    public class Acompañamiento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo nombre no puede exceder los 15 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo descripcion es requerido.")]
        [StringLength(maximumLength: 125, ErrorMessage = "El campo tipo no puede exceder los 125 caracteres")]
        [PrimeraLetraMayuscula]
        public string Descripcion { get; set; }

        public List<PlatilloAcompañamiento> PlatilloAcompañamiento { get; set; }
    }
}
