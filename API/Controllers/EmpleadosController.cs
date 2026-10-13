using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class EmpleadosController : Controller
    {

        private readonly DbEmpresaContext _context;

        public EmpleadosController(DbEmpresaContext context)
        {
            _context = context;
        }

        [HttpGet("ListarEmpleados")]
        public ActionResult<List<Empleado>> ListarEmpleados()
        {

            return _context.Empleados.ToList();

        }

        [HttpGet("ObtenerEmpleado/{id}")]
        public ActionResult<Empleado> ObtenerEmpleado(int id)
        {
            return _context.Empleados.FirstOrDefault(e => e.IdEmpleado == id);
        }


        [HttpPost("CreateEmpleado")]
        public async Task<IActionResult> CrearEmpleado([FromBody] Empleado nuevoEmpleado)
        {

            _context.Empleados.Add(nuevoEmpleado);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                msm = "Empleado creado exitosamente",
                cliente = nuevoEmpleado
            });
        }

        [HttpPut("ActualizarEmpleado/{id}")]
        public async Task<IActionResult> ActualizarEmpleado(int id, [FromBody] Empleado nuevoEmpleado)
        {

            if (id != nuevoEmpleado.IdEmpleado)
            {

                return BadRequest("El ID no existe");
            }

            var empleadoExiste = await _context.Empleados.FindAsync(id);

            if (empleadoExiste == null)
            {

                return NotFound("No se encontro al cliente con el ID");


            }

            empleadoExiste.Nombres = nuevoEmpleado.Nombres;
            empleadoExiste.IdPuesto = nuevoEmpleado.IdPuesto;
            empleadoExiste.Apellidos = nuevoEmpleado.Apellidos;

            await _context.SaveChangesAsync();


            return Ok(new
            {
                sms = "Cliente actualizado correctamente",
                empleado = nuevoEmpleado
            });

        }

    }


}
