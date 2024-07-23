using DatosCapa.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.Interfaces
{
    public interface IDireccion
    {
        Task<List<Dirección>> ObtenerDireciones();
        Task<string> CrearDireccion(int UsuarioId, string? Calle, string? Ciudad, string? Estado);
        Task<string> ActualizarDireccion(int Id, int UsuarioId, string? Calle, string? Ciudad, string? Estado);
        Task<Dirección> DireccionId(int id);
        Task<bool> EliminarDireccion(int id);
    }
}
