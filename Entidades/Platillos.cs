using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRestaurante.Entidades
{
    public class Platillos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo tipo es requerido.")]
        [StringLength(maximumLength:10, ErrorMessage = "El campo tipo no puede exceder los 10 caracteres")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo tipo no puede exceder los 15 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo precio es requerido.")]
        public int Precio { get; set; }

        [Required(ErrorMessage = "El campo descripcion es requerido.")]
        [StringLength(maximumLength: 125, ErrorMessage = "El campo tipo no puede exceder los 125 caracteres")]
        public string Descricpion { get; set; }

        [NotMapped]
        public int Kcalorias { get; set; }

        [Url]
        [NotMapped]
        public string Imagen { get; set; }



        public List<Acompañamiento> Acompañamientos { get; set; }
    }
}
