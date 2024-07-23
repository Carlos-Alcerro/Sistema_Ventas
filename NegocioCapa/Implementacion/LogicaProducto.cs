using DatosCapa.DataContext;
using Microsoft.EntityFrameworkCore;
using NegocioCapa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.Implementacion
{
    public class LogicaProducto : IProducto
    {
        private readonly SistemaPosContext _producto;
        public LogicaProducto(SistemaPosContext producto)
        {
            _producto = producto;
        }

        public async Task<string> ActualizarProducto(int id, string nombre, string descripcion, decimal? precio)
        {
            var producto = await _producto.Productos.FindAsync(id);
            if (producto == null)
                return "Producto no encontrado";

            if (nombre != null)
                producto.Nombre = nombre;
            if (descripcion != null)
                producto.Descripción = descripcion;
            if (precio.HasValue)
                producto.Precio = precio.Value;

            await _producto.SaveChangesAsync();
            return "Producto actualizado exitosamente";
        }

        public async Task<string> CrearProducto(string nombre, string descripcion, decimal precio)
        {
            var producto = new Producto
            {
                Nombre = nombre,
                Descripción = descripcion,
                Precio = precio
            };

            _producto.Productos.Add(producto);
            await _producto.SaveChangesAsync();
            return "Producto creado exitosamente";
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var producto = await _producto.Productos.FindAsync(id);
            if (producto == null)
                return false;

            _producto.Productos.Remove(producto);
            await _producto.SaveChangesAsync();
            return true;
        }

        public async Task<List<Producto>> ObtenerProducto()
        {
            return await _producto.Productos.ToListAsync();
        }

        public async Task<Producto> ProductoId(int id)
        {
            return await _producto.Productos.FindAsync(id);
        }
    }
}
