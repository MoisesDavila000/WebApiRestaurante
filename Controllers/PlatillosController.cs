using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante.Entidades;

namespace WebApiRestaurante.Controllers
{
    [ApiController]
    [Route("api/platillos")]
    public class PlatillosController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public PlatillosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/Menu")]
        public async Task<ActionResult<List<Platillos>>> Get()
        {
            return await dbContext.Platillos.Include(x => x.Acompañamientos).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Platillos>> PrimerPlatillo()
        {
            return await dbContext.Platillos.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Platillos>> Get([FromHeader] int id)
        {
            var platillo = await dbContext.Platillos.FirstOrDefaultAsync(x => x.Id == id);

            if(platillo == null)
            {
                return NotFound();
            }

            return platillo;

        }

        [HttpGet("{id:int}/{tipo}")]
        public async Task<ActionResult<Platillos>> Get([FromQuery] int id, [FromQuery] string tipo)
        {
            var platillo = await dbContext.Platillos.FirstOrDefaultAsync(x => x.Id == id && x.Tipo == tipo);

            if (platillo == null)
            {
                return NotFound("Algun valor ingresado no coincide con los datos almacenados.");
            }

            return platillo;

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Platillos>> Get([FromRoute] string nombre)
        {
            var platillo = await dbContext.Platillos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (platillo == null)
            {
                return NotFound();
            }

            return platillo;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Platillos platillo)
        {
            dbContext.Add(platillo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut ("{id:int}")]
        public async Task<ActionResult> Put(Platillos platillo, int id)
        {
            if(platillo.Id != id)
            {
                return BadRequest("El id del platillo no coincide con el establecido en la url.");
            }

            dbContext.Update(platillo);
            await dbContext.SaveChangesAsync();
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
            return Ok();
        }
    }
}
