using DatosCapa.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.Interfaces
{
    public interface IProducto
    {
        Task<List<Producto>> ObtenerProducto();
        Task<string> CrearProducto(string nombre, string descripcion, decimal precio);
        Task<string> ActualizarProducto(int id, string nombre, string descripcion, decimal? precio);

        Task<bool> EliminarProducto(int id);
        Task<Producto> ProductoId(int id);
    }
}
