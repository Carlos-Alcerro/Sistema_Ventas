using DatosCapa.DataContext;
using NegocioCapa.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.Interfaces
{
    public interface IUsuario
    {
        Task<List<Usuario>> ObtenerUsuario();
        Task<Usuario> AutenticarUsuario(string email, string contrasena);
        Task<string> CrearUsuario(string nombres, string apellidos, string sexo, string email, string contrasena, int? rol);
        Task<string> ActualizarUsuario(int id,string nombres, string apellidos, string sexo, string email, string contrasena, int? rol);

        Task<bool> EliminarUsuario(int id);
        Task<Usuario> UsuarioId(int id);
    }
}
