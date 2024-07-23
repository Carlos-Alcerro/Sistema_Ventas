using DatosCapa.DataContext;
using Microsoft.EntityFrameworkCore;
using NegocioCapa.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NegocioCapa.Implementacion
{
    public class LogicaDireccion : IDireccion
    {
        private readonly SistemaPosContext _context;

        public LogicaDireccion(SistemaPosContext context)
        {
            _context = context;
        }

        public async Task<string> ActualizarDireccion(int id, int usuarioId, string? calle, string? ciudad, string? estado)
        {
            var direccion = await _context.Direccións.FindAsync(id);
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

            await _context.SaveChangesAsync();
            return "Dirección actualizada exitosamente";
        }

        public async Task<string> CrearDireccion(int usuarioId, string? calle, string? ciudad, string? estado)
        {
            try
            {
                var direccion = new Dirección
                {
                    UsuarioId = usuarioId,
                    Calle = calle,
                    Ciudad = ciudad,
                    Estado = estado
                };

                _context.Direccións.Add(direccion);
                await _context.SaveChangesAsync();
                return "Dirección creada exitosamente";
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al crear la dirección: " + ex.Message);
            }
        }

        public async Task<Dirección> DireccionId(int id)
        {
            return await _context.Direccións.FindAsync(id);
        }

        public async Task<bool> EliminarDireccion(int id)
        {
            var direccionEncontrada = await _context.Direccións.FindAsync(id);
            if (direccionEncontrada == null)
            {
                throw new Exception("La dirección no se encontró");
            }

            _context.Direccións.Remove(direccionEncontrada);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Dirección>> ObtenerDireciones()
        {
            return await _context.Direccións.ToListAsync();
        }
    }
}
