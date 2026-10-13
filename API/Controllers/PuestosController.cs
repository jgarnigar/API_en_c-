using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class PuestosController : Controller
    {

        private readonly DbEmpresaContext _context;

        public PuestosController(DbEmpresaContext context)
        {
            _context = context;
        }

        [HttpGet("ListarPuestos")]
        public ActionResult<List<Puesto>> ListarPuestos()
        {

            return _context.Puestos.ToList();

        }

        [HttpGet("ObtenerPuesto/{id}")]
        public ActionResult<Puesto> ObtenerPuesto(int id)
        {
            return _context.Puestos.FirstOrDefault(e => e.IdPuesto == id);
        }

        [HttpPost("CrearPuesto")]
        public async Task<IActionResult> CrearPuesto([FromBody] Puesto nuevoPuesto)
        {

            _context.Puestos.Add(nuevoPuesto);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                msm = "Puesto creado exitosamente",
                cliente = nuevoPuesto
            });
        }


        [HttpPut("ActualizarPuesto/{id}")]
        public async Task<IActionResult> ActualizarEmpleado(int id, [FromBody] Puesto nuevoPuesto)
        {

            if (id != nuevoPuesto.IdPuesto)
            {

                return BadRequest("El ID no existe");
            }

            var puestoExiste = await _context.Puestos.FindAsync(id);

            if (puestoExiste == null)
            {

                return NotFound("No se encontro al cliente con el ID");


            }

            puestoExiste.Nombre = nuevoPuesto.Nombre;

            await _context.SaveChangesAsync();


            return Ok(new
            {
                sms = "Cliente actualizado correctamente",
                empleado = nuevoPuesto
            });

        }
    }
}