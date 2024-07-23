using DatosCapa.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NegocioCapa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NegocioCapa.Implementacion
{
    public class LogicaPedido : IPedido
    {
        private readonly SistemaPosContext _context;
        public LogicaPedido(SistemaPosContext context)
        {
            _context = context;
        }
        public async Task<string> ActualizarPedido(int id, int usuarioId, int direccionId, string producto, decimal total)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(id);
                if (pedido == null)
                {
                    return "No se encontró el pedido";
                }

                pedido.UsuarioId = usuarioId;
                pedido.DirecciónId = direccionId;

                if (!string.IsNullOrEmpty(producto))
                {
                    pedido.Producto = producto;
                }

                if (total > 0)
                {
                    pedido.Total = total;
                }

                await _context.SaveChangesAsync();
                return "Pedido actualizado exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al actualizar el pedido", ex);
            }
        }

        public async Task<string> CrearPedido(int usuarioId, int direccionId, string producto, decimal total)
        {
            try
            {
                var pedido = new Pedido
                {
                   UsuarioId = usuarioId,
                   DirecciónId = direccionId,
                   Fecha = DateTime.Now,
                   Producto = producto,
                   Total = total
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                return "Pedido creado exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al crear el pedido", ex);
            }
        }

        public async Task<bool> EliminarPedido(int id)
        {
            var pedidoEncontrado = await _context.Pedidos.FindAsync(id);
            if (pedidoEncontrado == null)
            {
                return false;
            }
            _context.Pedidos.Remove(pedidoEncontrado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Pedido>> ObtenerPedidos()
        {
            try
            {
                var pedidos = await _context.Pedidos.ToListAsync();
                if (pedidos == null || pedidos.Count == 0)
                {
                    return new List<Pedido>();
                }
                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al obtener los pedidos", ex);
            }

        }

        public async Task<Pedido> PedidoId(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }
    }
}
