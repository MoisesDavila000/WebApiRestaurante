using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRestaurante.Entidades
{
    public class Acompañamiento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo nombre no puede exceder los 15 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo descripcion es requerido.")]
        [StringLength(maximumLength: 125, ErrorMessage = "El campo tipo no puede exceder los 125 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo platilloId es requerido.")]
        public int PlatilloId { get; set; }

        [NotMapped]
        public int Kcalorias { get; set; }

        [Url]
        [NotMapped]
        public string Imagen { get; set; }

        public Platillos Platillo { get; set; }  
    }
}
