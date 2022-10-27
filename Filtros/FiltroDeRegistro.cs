using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiRestaurante.Filtros
{
    public class FiltroDeRegistro :IActionFilter
    {
        private readonly ILogger<FiltroDeRegistro> log;

        public FiltroDeRegistro(ILogger<FiltroDeRegistro> log)
        {
            this.log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.LogInformation("Se realizara un registro nuevo.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            log.LogInformation("Se realizo un registro de manera exitosa.");
        }
    }
}
