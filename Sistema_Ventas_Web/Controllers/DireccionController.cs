using DatosCapa.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioCapa.DTOs;
using NegocioCapa.Interfaces;

namespace SistemaPOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly IDireccion _direccion;
        public DireccionController(IDireccion direccion)
        {
            _direccion = direccion;
        }

        //Este endpoint obtiene todas las direcciones de Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dirección>>> GetDirecciones()
        {
            var direcciones = await _direccion.ObtenerDireciones();
            return Ok(direcciones);
        }

        //Este endpoint obtiene direccion por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Dirección>> GetDireccion(int id)
        {
            var direccion = await _direccion.DireccionId(id);
            if (direccion == null)
            {
                return NotFound();
            }
            return Ok(direccion);
        }

        //Con este endpoint se crean las direccions de cada cliente
        [HttpPost]
        public async Task<ActionResult> PostDireccion([FromBody] DTODireccion direccionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _direccion.CrearDireccion(
                direccionDto.UsuarioId,
                direccionDto.Calle,
                direccionDto.Ciudad,
                direccionDto.Estado
            );
                return Ok(result);
            }
            catch 
            {
                return BadRequest("Hubo un problema");
            }
        }

        //Con este endpoint se actualizan las direcciones 
        [HttpPut("{id}")]
        public async Task<string> ActualizarDireccion(int id, int UsuarioId, string? calle, string? ciudad, string? estado)
        {
            var direccion = await _direccion.DireccionId(id);
            if (direccion == null)
            {
                return "No se encontró la dirección";
            }

            if (calle != null)
                direccion.Calle = calle;
            if (ciudad != null)
                direccion.Ciudad = ciudad;
            if (estado != null)
                direccion.Estado = estado;

            await _direccion.ActualizarDireccion(id, UsuarioId, calle, ciudad, estado);
            return "Dirección actualizada exitosamente";
        }


        //Con este endpoint se eliminan direcciones por ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDireccion(int id)
        {
            try
            {
                await _direccion.EliminarDireccion(id);
                return Ok();
            }
            catch 
            {
                return NotFound("Ocurrio un problema");
            }
        }
    }
}
