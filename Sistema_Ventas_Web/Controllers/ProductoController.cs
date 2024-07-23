using Microsoft.AspNetCore.Mvc;
using NegocioCapa.Interfaces;
using DatosCapa.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using NegocioCapa.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaPOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProducto _productoService;

        public ProductoController(IProducto productoService)
        {
            _productoService = productoService;
        }
        //Con este endpoint se obtienen todos los productos de los clientes

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoService.ObtenerProducto();
            return Ok(productos);
        }

        //Con este endpoint se obtiene solamente un producto
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoService.ProductoId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
        //Con este endpoint se realiza la creacion de productos
        [HttpPost]
        public async Task<ActionResult> PostProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productoService.CrearProducto(
                producto.Nombre,
                producto.Descripción,
                producto.Precio
            );

            return Ok(result);
        }

        //Con este endpoint se actualizan o modifican las propiedades de los productos
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProducto(int id, string? nombre, string? descripcion, decimal? precio)
        {
            var producto = await _productoService.ProductoId(id);
            if (producto == null)
                return NotFound();

            if (nombre != null)
                producto.Nombre = nombre;

            if (descripcion != null)
                producto.Descripción = descripcion;

            if (precio.HasValue)
                producto.Precio = precio.Value;

            await _productoService.ActualizarProducto( id,nombre,descripcion,precio);
            return Ok();
        }

        //Con este endpoint se eliminan productos por su ID de identificacion
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            var result = await _productoService.EliminarProducto(id);
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
