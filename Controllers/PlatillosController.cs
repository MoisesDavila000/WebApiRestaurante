using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante.Entidades;

namespace WebApiRestaurante.Controllers
{
    [ApiController]
    [Route("Platillos")]
    public class PlatillosController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public PlatillosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Platillos>>> Get()
        {
            return await dbContext.Platillos.Include(x => x.Acompañamientos).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Platillos platillo)
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
