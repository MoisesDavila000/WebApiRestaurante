using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante2.DTOs;
using WebApiRestaurante2.Entidades;
using WebApiRestaurante2.Filtros;
using WebApiRestaurante2.Services;

namespace WebApiRestaurante2.Controllers
{
    [ApiController]
    [Route("Platillos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class PlatillosController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PlatillosController> logger;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public PlatillosController(ApplicationDbContext dbContext, ILogger<PlatillosController> logger, IWebHostEnvironment env, IMapper mapper)
        {
            
            this.dbContext = dbContext;
            this.logger = logger;
            this.env = env;
            this.mapper = mapper;
            
        }

        [HttpGet("/Menu_Platillos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GETPlatilloDTO>>> Get()
        {
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            logger.LogInformation("Se obtiene el listado de los platillos.");
            var platillos = await dbContext.Platillos.ToListAsync();
            return mapper.Map<List<GETPlatilloDTO>>(platillos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlatilloDTOConAcompañamiento>> GetById(int id)
        {
            var platillo = await dbContext.Platillos
                .Include(platilloDB => platilloDB.PlatilloAcompañamientos)
                .ThenInclude(platilloAcompañamientoDB => platilloAcompañamientoDB.Acompañamiento)
                .FirstOrDefaultAsync(platilloBD => platilloBD.Id == id);

            if(platillo == null)
            {
                logger.LogError("No se encuentra el platillo con dicho id.");
                return NotFound();
            }

            platillo.PlatilloAcompañamientos = platillo.PlatilloAcompañamientos.OrderBy(x => x.Orden).ToList();

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();

            return mapper.Map<PlatilloDTOConAcompañamiento>(platillo);

        }


        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GETPlatilloDTO>>> Get([FromRoute] string nombre)
        {
            var platillo = await dbContext.Platillos.Where(platilloBD => platilloBD.Nombre.Contains(nombre)).ToListAsync();

            if (platillo == null)
            {
                logger.LogError("No se encuentra el platillo con dicho nombre.");
                return NotFound();
            }

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();

            return mapper.Map<List<GETPlatilloDTO>>(platillo);

        }

        [HttpPost]
        [ServiceFilter(typeof(FiltroDeRegistro))]
        public async Task<ActionResult> Post(PlatilloDTO platilloDTO)
        {
            var existePLatillo = await dbContext.Platillos.AnyAsync(x => x.Nombre == platilloDTO.Nombre);

            if (existePLatillo)
            {
                return BadRequest("Ya existe un registro con el mismo nombre");
            }

            var platillo = mapper.Map<Platillos>(platilloDTO);

            dbContext.Add(platillo);
            await dbContext.SaveChangesAsync();

            var getplatilloDTO = mapper.Map<GETPlatilloDTO>(platillo);

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPost();
            return NoContent();
        }

        [ServiceFilter(typeof(FiltroDeRegistro))]
        [HttpPut ("{id:int}")]
        public async Task<ActionResult> Put(PlatilloDTO platilloCreacionDTO, int id)
        {
            var exist = await dbContext.Platillos.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return BadRequest("El id del platillo no coincide con el establecido en la url.");
            }
            var platilloDB = await dbContext.Platillos
                .Include(x => x.PlatilloAcompañamientos)
                .FirstOrDefaultAsync(x => x.Id == id);

            platilloDB = mapper.Map(platilloCreacionDTO, platilloDB);
            OrdenarPorAcompañamientos(platilloDB);

            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPut();
            return NoContent();
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

        private void OrdenarPorAcompañamientos(Platillos platillo)
        {
            if (platillo.PlatilloAcompañamientos != null)
            {
                for (int i = 0; i < platillo.PlatilloAcompañamientos.Count; i++)
                {
                    platillo.PlatilloAcompañamientos[i].Orden = i;
                }
            }
        }
    }
}
