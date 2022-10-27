using System.ComponentModel.DataAnnotations;
using WebApiRestaurante.Entidades;

namespace WebApiRestaurante.Validaciones
{
    public class ValorDentroDeRangoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            int valorDentro = int.Parse(value.ToString());

            if (valorDentro < 100 || valorDentro >  300)
            {
                return new ValidationResult("El valor ingresado no entra en el rango de $100 a $300.");
            }

            return ValidationResult.Success;
        }

    }
}
