using DatosCapa.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.Interfaces
{
    public interface IPedido
    {
        Task<List<Pedido>> ObtenerPedidos();
        Task<string> CrearPedido(int usuarioId, int direccionId,string producto,decimal total);
        Task<string> ActualizarPedido(int id, int usuarioId, int direccionId, string producto, decimal total);

        Task<bool> EliminarPedido(int id);
        Task<Pedido> PedidoId(int id);
    }
}
