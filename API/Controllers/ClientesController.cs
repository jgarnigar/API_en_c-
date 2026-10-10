using API.Models;
using Microsoft.AspNetCore.Mvc;

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

    }
}
