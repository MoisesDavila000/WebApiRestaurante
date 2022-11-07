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
    [Route("Turnos")]
    public class TurnoController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<TurnoController> logger;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public TurnoController(ApplicationDbContext dbContext, ILogger<TurnoController> logger, IWebHostEnvironment env, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.env = env;
            this.mapper = mapper;
        }

        /*[HttpGet("Listado")]
        public async Task<ActionResult<List<GETTurnoDTO>>> GetAll()
        {
            var turno = await dbContext.Turnos
                .Include(empleadosDB => empleadosDB.Empleados)
                .FirstOrDefaultAsync(x => x.Id == id);

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            logger.LogInformation("Se obtiene el listado de los turnos.");
            return await dbContext.Turnos.Include(x => x.Empleados).ToListAsync();
        }
        */

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GETTurnoDTO>> Get([FromHeader] int id)
        {
            var turno = await dbContext.Turnos
                .Include(empleadosDB => empleadosDB.Empleados)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (turno == null)
            {
                logger.LogError("No se encuentra el turno con dicho id.");
                return NotFound();
            }

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();

            return mapper.Map<GETTurnoDTO>(turno); ;

        }

        [HttpPost]
        public async Task<ActionResult> Post(TurnoDTO turnoDTO)
        {
            var existeEntrada = await dbContext.Turnos.AnyAsync(x => x.Entrada == turnoDTO.Entrada);
            var existeSalida = await dbContext.Turnos.AnyAsync(x => x.Salida == turnoDTO.Salida);

            if (existeEntrada && existeSalida)
            {
                return BadRequest("Ya existe un turno con la misma hora de trabajo");
            }

            var turno = mapper.Map<Turnos>(turnoDTO);

            dbContext.Add(turno);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPost();

            var getturnoDTO = mapper.Map<GETTurnoDTO>(turno);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TurnoDTO turnoDTO, int id)
        {
            var existeTurno = await dbContext.Turnos.AnyAsync(turnoDB => turnoDB.Id == id);
            if (existeTurno)
            {
                return BadRequest("El id del turno no coincide con el establecido en la url.");
            }

            var turno = mapper.Map<Turnos>(turnoDTO);
            turno.Id = id;

            dbContext.Update(turno);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPut();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Turnos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Turnos()
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
