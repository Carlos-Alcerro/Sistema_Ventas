using DatosCapa.DataContext;
using Microsoft.AspNetCore.Mvc;
using NegocioCapa.Interfaces;
using NegocioCapa.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaPOS.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedido _pedido;
        public PedidoController(IPedido pedido)
        {
            _pedido = pedido;
        }
        //Con este endpoint se obtienen todos los pedidos de los clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _pedido.ObtenerPedidos();
            return Ok(pedidos);
        }

        //Con este endpoint se puede obtener solamente 1 pedidos
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoId(int id)
        {
            var pedido = await _pedido.PedidoId(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        //Con este endpoint se crean los pedidos de los clientes
        [HttpPost]
        public async Task<ActionResult> PostPedido([FromBody] DTOPedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _pedido.CrearPedido(pedido.UsuarioId,pedido.DirecciónId,pedido.Producto,pedido.Total);
            return Ok(resultado);
        }

        //Con este endpoint se actualizan los pedidos
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPedido(int id, int usuarioId, int direccionId, string producto, decimal total)
        {
            var pedido = await _pedido.PedidoId(id);
            if (pedido == null)
                return NotFound();

            if (usuarioId != null)
                pedido.UsuarioId = usuarioId;

            if (direccionId != null)
                pedido.DirecciónId = direccionId;
            if (producto != null)
                pedido.Producto = producto;
            if (total != null)
                pedido.Total = total;



            await _pedido.ActualizarPedido(id, usuarioId, direccionId,producto,total);
            return Ok();
        }

        //Con este endpoint se eliminan pedidos de clientes
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePedido(int id)
        {
            var result = await _pedido.EliminarPedido(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
