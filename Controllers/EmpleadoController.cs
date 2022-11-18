using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestaurante2.Entidades;
using WebApiRestaurante2.Filtros;
using WebApiRestaurante2.Services;
using WebApiRestaurante2.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApiRestaurante2.Controllers
{
    [ApiController]
    [Route("Empleados")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class EmpleadoController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<EmpleadoController> logger;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public EmpleadoController (ApplicationDbContext dbContext, ILogger<EmpleadoController> logger, IWebHostEnvironment env, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.env = env;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GETEmpleadoDTO>>> Get(int turnoId)
        {
            var existeTurno = await dbContext.Turnos.AnyAsync(turnoDB => turnoDB.Id == turnoId);

            if (!existeTurno)
            {
                return NotFound();
            }

            var empleados = await dbContext.Empleados.Where(empleadoDB => empleadoDB.TurnosId == turnoId).ToListAsync();


            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            logger.LogInformation("Se obtiene el listado de los empleados.");
            return mapper.Map<List<GETEmpleadoDTO>>(empleados);
        }

        [HttpGet("{matricula:int}")]
        public async Task<ActionResult<GETEmpleadoDTO>> GetById([FromRoute] int matricula)
        {

            var empleado = await dbContext.Empleados.FirstOrDefaultAsync(x => x.Matricula == matricula);

            if (empleado == null)
            {
                logger.LogError("No se encuentra el empelado con dicho id.");
                return NotFound();
            }

            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetGet();
            return mapper.Map<GETEmpleadoDTO>(empleado);
        }

        [HttpPost]
        public async Task<ActionResult> Post( int turnoId, EmpleadoDTO empleadoDTO)
        {

            var existeTurno = await dbContext.Turnos.AnyAsync(turnoDB => turnoDB.Id == turnoId);

            if (!existeTurno)
            {
                NotFound();
            }

            var empleado = mapper.Map<Empleados>(empleadoDTO);
            empleado.TurnosId = turnoId;
            dbContext.Add(empleado);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPost();
            
            var getempleadoDTO = mapper.Map<GETEmpleadoDTO>(empleado);

            return NoContent();

        }

        [HttpPut("{matricula:int}")]
        public async Task<ActionResult> Put(int turnoId, int matricula, EmpleadoDTO empleadoDTO)
        {
            var existeTurno = await dbContext.Turnos.AnyAsync(x => x.Id == turnoId);

            if (!existeTurno)
            {
                return NotFound();
            }

            var existeEmpleado = await dbContext.Empleados.AnyAsync(x => x.Matricula == matricula);
            if (!existeEmpleado)
            {
                return BadRequest();
            }

            var empleado = mapper.Map<Empleados>(empleadoDTO);

            dbContext.Update(empleado);
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetPut();
            return NoContent();
        }

        [HttpDelete("{matricula:int}")]
        public async Task<ActionResult> Delete(int matricula)
        {
            var exist = await dbContext.Empleados.AnyAsync(x => x.Matricula == matricula);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Empleados()
            {
                Matricula = matricula
            });
            await dbContext.SaveChangesAsync();
            EscribirArchivo escribir = new EscribirArchivo(env);
            escribir.PetDelete();
            return Ok();
        }
    }
}
