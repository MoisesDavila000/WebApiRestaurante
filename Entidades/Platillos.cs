using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiRestaurante.Validaciones;

namespace WebApiRestaurante.Entidades
{
    public class Platillos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo tipo es requerido.")]
        [StringLength(maximumLength:10, ErrorMessage = "El campo tipo no puede exceder los 10 caracteres")]
        [PrimeraLetraMayuscula]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo tipo no puede exceder los 15 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo precio es requerido.")]
        [ValorDentroDeRango]
        public int Precio { get; set; }

        [Required(ErrorMessage = "El campo descripcion es requerido.")]
        [StringLength(maximumLength: 125, ErrorMessage = "El campo tipo no puede exceder los 125 caracteres")]
        [PrimeraLetraMayuscula]
        public string Descricpion { get; set; }

        [NotMapped]
        public int Kcalorias { get; set; }

        [Url]
        [NotMapped]
        public string Imagen { get; set; }



        public List<Acompañamiento> Acompañamientos { get; set; }
    }
}
