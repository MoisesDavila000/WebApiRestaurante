using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante.Entidades;
using WebApiRestaurante.Filtros;
using WebApiRestaurante.Services;

namespace WebApiRestaurante.Controllers
{
    [ApiController]
    [Route("Acompañamientos")]
    public class AcompañamientoController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PlatillosController> logger;
        private readonly IWebHostEnvironment env;

        public AcompañamientoController (ApplicationDbContext dbContext, ILogger<PlatillosController> logger, IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.env = env;
        }

        [HttpGet]
        public async Task<ActionResult<List<Acompañamiento>>> GetAll()
        {
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            logger.LogInformation("Se obtiene el listado de los acompañamientos.");
            return await dbContext.Acompañamientos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Acompañamiento>> GetById([FromHeader] int id)
        {

            var acompañamiento = await dbContext.Acompañamientos.FirstOrDefaultAsync(x => x.Id == id);

            if (acompañamiento == null)
            {
                logger.LogError("No se encuentra el acompañamiento con dicho id.");
                return NotFound();
            }

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            return await dbContext.Acompañamientos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Acompañamiento>> Get([FromRoute] string nombre)
        {
            var acompañamiento = await dbContext.Acompañamientos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (acompañamiento == null)
            {
                logger.LogError("No se encuentra el acompañamiento con dicho nombre.");
                return NotFound();
            }
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            return acompañamiento;

        }

        [HttpPost]
        [ServiceFilter(typeof(FiltroDeRegistro))]
        public async Task<ActionResult> Post ([FromBody] Acompañamiento acompañamiento)
        {

            var existeAcompañamiento = await dbContext.Acompañamientos.AnyAsync(x => x.Nombre == acompañamiento.Nombre);

            if (existeAcompañamiento)
            {
                logger.LogError("Ya existe un acompañamiento con dichos datos.");
                return BadRequest("Ya existe un registro con el mismo nombre");
            }

            var existePlatillo = await dbContext.Platillos.AnyAsync(x => x.Id == acompañamiento.PlatilloId);

            if (!existePlatillo)
            {
                logger.LogError("No existe un platillo con la id ingresada.");
                return BadRequest($"No existe el platillo con el id: {acompañamiento.PlatilloId}");
            }

            dbContext.Add(acompañamiento);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPost();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(FiltroDeRegistro))]
        public async Task<ActionResult> Put(Acompañamiento acompañamiento, int id)
        {
            var exist = await dbContext.Acompañamientos.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                logger.LogError("Acompañamiento buscado no existe.");
                return NotFound("El acompañamiento especificado no existe");
            }

            if (acompañamiento.Id != id)
            {
                logger.LogError("El id del acompañamiento no coincide con el establecido en la url.");
                return BadRequest("El id del acompañamiento no coincide con el establecido en la url.");
            }

            dbContext.Update(acompañamiento);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPut();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Acompañamientos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Acompañamiento()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetDelete();
            return Ok();
        }
    }
}
