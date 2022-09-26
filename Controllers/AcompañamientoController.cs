using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante.Entidades;

namespace WebApiRestaurante.Controllers
{
    [ApiController]
    [Route("Acompañamientos")]
    public class AcompañamientoController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AcompañamientoController (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Acompañamiento>>> GetAll()
        {
            return await dbContext.Acompañamientos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Acompañamiento>> GetById([FromHeader] int id)
        {
            return await dbContext.Acompañamientos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Acompañamiento>> Get([FromRoute] string nombre)
        {
            var acompañamiento = await dbContext.Acompañamientos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (acompañamiento == null)
            {
                return NotFound();
            }

            return acompañamiento;

        }

        [HttpPost]
        public async Task<ActionResult> Post ([FromBody] Acompañamiento acompañamiento)
        {
            var existePlatillo = await dbContext.Platillos.AnyAsync(x => x.Id == acompañamiento.PlatilloId);

            if (!existePlatillo)
            {
                return BadRequest($"No existe el platillo con el id: {acompañamiento.PlatilloId}");
            }

            dbContext.Add(acompañamiento);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Acompañamiento acompañamiento, int id)
        {
            var exist = await dbContext.Acompañamientos.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El acompañamiento especificado no existe");
            }

            if (acompañamiento.Id != id)
            {
                return BadRequest("El id del acompañamiento no coincide con el establecido en la url.");
            }

            dbContext.Update(acompañamiento);
            await dbContext.SaveChangesAsync();
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
            return Ok();
        }
    }
}
