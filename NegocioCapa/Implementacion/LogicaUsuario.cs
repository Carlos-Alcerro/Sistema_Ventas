using DatosCapa.DataContext;
using Microsoft.EntityFrameworkCore;
using NegocioCapa.DTOs;
using NegocioCapa.Interfaces;
using NegocioCapa.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.Implementacion
{
    public class LogicaUsuario : IUsuario
    {
        private readonly SistemaPosContext _context;
        private readonly TokenHelper _tokenHelper;
        public LogicaUsuario(SistemaPosContext context)
        {
            _context = context;
        }

        public async Task<string> CrearUsuario(string nombres, string apellidos, string sexo, string email, string contrasena, int? rol)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(contrasena);
            Usuario usuario = new Usuario
            {
                Nombres = nombres,
                Apellidos = apellidos,
                Sexo = sexo,
                Email = email,
                Contrasena = hashedPassword,
                RolId = rol ?? 2
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return "Usuaio Creado exitosamente";
            
        }
        public async Task<string> ActualizarUsuario(int id, string nombres, string apellidos, string sexo, string email, string contrasena, int? rol)
        {
            
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return "No se encontró el usuario";


            usuario.Nombres = nombres;
            usuario.Apellidos = apellidos;
            usuario.Sexo = sexo;

            if (!string.IsNullOrEmpty(contrasena))
            {
                usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(contrasena);
            }

            if (rol.HasValue)
            {
                usuario.RolId = rol.Value;
            }

            await _context.SaveChangesAsync();
            return "Usuario Actualizado Exitosamente";
        }


        public async Task<bool> EliminarUsuario(int id)
        {
            var usuarioEncontrado = await _context.Usuarios.FindAsync(id);
            if(usuarioEncontrado == null)
            {
                throw new KeyNotFoundException("El usuario no existe");
            }
            _context.Usuarios.Remove(usuarioEncontrado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Usuario>> ObtenerUsuario()
        {
            return await _context.Usuarios.Include(u=>u.Direccións).ToListAsync();
        }

        public async Task<Usuario> UsuarioId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> AutenticarUsuario(string email, string contrasena)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
            {
                return null; 
            }

            return usuario;
        }
    }
}
