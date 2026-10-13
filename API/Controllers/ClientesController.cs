using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private readonly DbEmpresaContext _context;
        public ClientesController(DbEmpresaContext context)
        {
            _context = context;
        }

        [HttpGet("ListarUsuarios")]
        public ActionResult<List<Cliente>> ListarClietnes()
        {
            return _context.Clientes.ToList();
        }

        [HttpGet("ObtenerUsuario/{id}")]
        public ActionResult<Cliente> ObtenerUsuario(int id)
        {
            return _context.Clientes.FirstOrDefault(e => e.IdCliente == id);
        }

        //POST

        [HttpPost("CreateCliente")]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente nuevoCliente)
        {

            _context.Clientes.Add(nuevoCliente);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                msm = "Cliente creado exitosamente",
                cliente = nuevoCliente
            });
        }

        [HttpPut("ActualizarCliente/{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Cliente nuevoCliente){

            if(id != nuevoCliente.IdCliente){

                return BadRequest("El ID no existe");
            }

            var clienteExiste = await _context.Clientes.FindAsync(id);

            if (clienteExiste == null) {

                return NotFound("No se encontro al cliente con el ID");


            }

            clienteExiste.Nombres = nuevoCliente.Nombres;
            clienteExiste.Apellidos = nuevoCliente.Apellidos;
            clienteExiste.Telefono = nuevoCliente.Telefono;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                sms = "Cliente actualizado correctamente",
                cliente = nuevoCliente
            });

        }

    }
}
