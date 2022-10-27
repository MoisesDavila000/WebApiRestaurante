using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante.Entidades;
using WebApiRestaurante.Filtros;
using WebApiRestaurante.Services;

namespace WebApiRestaurante.Controllers
{
    [ApiController]
    [Route("api/platillos")]
    public class PlatillosController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PlatillosController> logger;
        private readonly IWebHostEnvironment env;
        //private readonly EscribirArchivo escribir;

        public PlatillosController(ApplicationDbContext dbContext, ILogger<PlatillosController> logger, IWebHostEnvironment env)
        {
            
            this.dbContext = dbContext;
            this.logger = logger;
            this.env = env;
            
        }


        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/Menu")]
        public async Task<ActionResult<List<Platillos>>> Get()
        {
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            logger.LogInformation("Se obtiene el listado de los platillos.");
            return await dbContext.Platillos.Include(x => x.Acompañamientos).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Platillos>> PrimerPlatillo()
        {
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            logger.LogInformation("Se obtiene el primer platillo.");
            return await dbContext.Platillos.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Platillos>> Get([FromHeader] int id)
        {
            var platillo = await dbContext.Platillos.FirstOrDefaultAsync(x => x.Id == id);

            if(platillo == null)
            {
                logger.LogError("No se encuentra el platillo con dicho id.");
                return NotFound();
            }
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();

            return platillo;

        }

        [HttpGet("{id:int}/{tipo}")]
        public async Task<ActionResult<Platillos>> Get([FromQuery] int id, [FromQuery] string tipo)
        {
            var platillo = await dbContext.Platillos.FirstOrDefaultAsync(x => x.Id == id && x.Tipo == tipo);

            if (platillo == null)
            {
                logger.LogError("No se encuentra el platillo con dichos datos.");
                return NotFound("Algun valor ingresado no coincide con los datos almacenados.");
            }

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();

            return platillo;

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Platillos>> Get([FromRoute] string nombre)
        {
            var platillo = await dbContext.Platillos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (platillo == null)
            {
                logger.LogError("No se encuentra el platillo con dicho nombre.");
                return NotFound();
            }

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();

            return platillo;

        }

        [HttpPost]
        [ServiceFilter(typeof(FiltroDeRegistro))]
        public async Task<ActionResult> Post([FromBody] Platillos platillo)
        {
            var existePLatillo = await dbContext.Platillos.AnyAsync(x => x.Nombre == platillo.Nombre);

            if (existePLatillo)
            {
                return BadRequest("Ya existe un registro con el mismo nombre");
            }

            dbContext.Add(platillo);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPost();
            return Ok();
        }

        [ServiceFilter(typeof(FiltroDeRegistro))]
        [HttpPut ("{id:int}")]
        public async Task<ActionResult> Put(Platillos platillo, int id)
        {
            if(platillo.Id != id)
            {
                return BadRequest("El id del platillo no coincide con el establecido en la url.");
            }

            if (platillo.Precio < 100 || platillo.Precio > 300)
            {
                return BadRequest("El valor ingresado no entra en el rango de $100 a $300.");
            }

            dbContext.Update(platillo);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPut();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Platillos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Platillos()
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
