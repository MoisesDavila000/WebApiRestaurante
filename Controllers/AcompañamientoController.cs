using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante2.DTOs;
using WebApiRestaurante2.Entidades;
using WebApiRestaurante2.Filtros;
using WebApiRestaurante2.Services;

namespace WebApiRestaurante2.Controllers
{
    [ApiController]
    [Route("Acompañamientos")]
    public class AcompañamientoController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PlatillosController> logger;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public AcompañamientoController (ApplicationDbContext dbContext, ILogger<PlatillosController> logger, IWebHostEnvironment env, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.env = env;
            this.mapper = mapper;
        }

        [HttpGet("/Menu_Acompañamientos")]
        public async Task<ActionResult<List<GETAcompañamientoDTO>>> GetAll()
        {
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            logger.LogInformation("Se obtiene el listado de los acompañamientos.");
            var acompañamiento = await dbContext.Acompañamientos.ToListAsync();
            return mapper.Map<List<GETAcompañamientoDTO>>(acompañamiento);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AcompañamientoDTOConPlatillo>> GetById(int id)
        {

            var acompañamiento = await dbContext.Acompañamientos
                .Include(acompañamientoDB => acompañamientoDB.PlatilloAcompañamiento)
                .ThenInclude(platilloAcompañamiento => platilloAcompañamiento.Platillos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (acompañamiento == null)
            {
                logger.LogError("No se encuentra el acompañamiento con dicho id.");
                return NotFound();
            }

            acompañamiento.PlatilloAcompañamiento = acompañamiento.PlatilloAcompañamiento.OrderBy(x => x.Orden).ToList();

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            return mapper.Map<AcompañamientoDTOConPlatillo>(acompañamiento);
        }

        /*[HttpGet("{nombre}")]
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
        */

        [HttpPost]
        [ServiceFilter(typeof(FiltroDeRegistro))]
        public async Task<ActionResult> Post (AcompañamientoDTO acompañamientoDTO)
        {
            if(acompañamientoDTO.PlatillosIds == null)
            {
                return BadRequest("No se puede crear un acompañamiento sin platillos.");
            }

            var platillosIds = await dbContext.Platillos
                .Where(platillosBD => acompañamientoDTO.PlatillosIds.Contains(platillosBD.Id)).Select(x => x.Id).ToListAsync();

            if(acompañamientoDTO.PlatillosIds.Count != platillosIds.Count)
            {
                return BadRequest("No existe uno de los platillos enviados");
            }

            /*var existeAcompañamiento = await dbContext.Acompañamientos.AnyAsync(x => x.Nombre == acompañamiento.Nombre);

            if (existeAcompañamiento)
            {
                logger.LogError("Ya existe un acompañamiento con dichos datos.");
                return BadRequest("Ya existe un registro con el mismo nombre");
            }
            */
            //var existePlatillo = await dbContext.Platillos.AnyAsync(x => x.Id == acompañamiento.PlatilloId);

            /*if (!existePlatillo)
            {
                logger.LogError("No existe un platillo con la id ingresada.");
                return BadRequest($"No existe el platillo con el id: {acompañamiento.PlatilloId}");
            }*/

            var acompañamiento = mapper.Map<Acompañamiento>(acompañamientoDTO);

            dbContext.Add(acompañamiento);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPost();

            var acompDTO = mapper.Map<AcompañamientoDTO>(acompañamiento);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(FiltroDeRegistro))]
        public async Task<ActionResult> Put(AcompañamientoDTO acompañamientoDTO, int id)
        {
            var acompañamientoDB = await dbContext.Acompañamientos
                .Include(x => x.PlatilloAcompañamiento)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (acompañamientoDB == null)
            {
                logger.LogError("Acompañamiento buscado no existe.");
                return NotFound("El acompañamiento especificado no existe");
            }

            //if (acompañamiento.Id != id)
            //{
            //    logger.LogError("El id del acompañamiento no coincide con el establecido en la url.");
            //    return BadRequest("El id del acompañamiento no coincide con el establecido en la url.");
            //}

            acompañamientoDB = mapper.Map(acompañamientoDTO, acompañamientoDB);
            OrdenarPorPlatillos(acompañamientoDB);

            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPut();
            return NoContent();
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

        private void OrdenarPorPlatillos(Acompañamiento acompañamiento)
        {
            if(acompañamiento.PlatilloAcompañamiento != null)
            {
                for (int i = 0; i < acompañamiento.PlatilloAcompañamiento.Count; i++)
                {
                    acompañamiento.PlatilloAcompañamiento[i].Orden = i;
                }
            }
        }
    }
}
