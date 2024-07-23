using Microsoft.AspNetCore.Mvc;
using NegocioCapa.Interfaces;
using DatosCapa.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using NegocioCapa.DTOs;
using NegocioCapa.Helpers;
using Microsoft.AspNetCore.Identity.Data;


namespace SistemaPOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioService;
        private readonly TokenHelper _tokenHelper;

        public UsuarioController(IUsuario usuarioService, TokenHelper tokenHelper)
        {
            _usuarioService = usuarioService;
            _tokenHelper = tokenHelper;

        }
        //Con este endpoint se obtienen todos los usuarios
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuario();
            return Ok(usuarios);
        }


        //Con este endpoint se obtiene un solo usuario por medio de su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.UsuarioId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        //Con este endpoint se crean o registran los usuarios en el sistema
        [HttpPost("register")]
        public async Task<ActionResult> PostUsuario([FromBody] DTOUsuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rolId = usuario.RolId ?? 2;

                var result = await _usuarioService.CrearUsuario(
                    usuario.Nombres,
                    usuario.Apellidos,
                    usuario.Sexo,
                    usuario.Email,
                    usuario.Contrasena,
                    rolId
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //Con este endpoint se actualizan usuarios individualmente por su identificador
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUsuario(int id, [FromBody] DTOUsuario usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usuarioService.ActualizarUsuario(
                id,
                usuarioDto.Nombres,
                usuarioDto.Apellidos,
                usuarioDto.Sexo,
                usuarioDto.Email,
                usuarioDto.Contrasena,
                usuarioDto.RolId
            );

            if (result == "Usuario Actualizado Exitosamente")
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result);
            }
        }

        //Con este endpoint se eliminan usuarios del sistema
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioService.EliminarUsuario(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        //Este endpopint sirve para la autenticacion de usuario, ya que solo solicita en correo y contrasena
        //si estas coinciden se le da acceso al sistema y tambien se le genera un JWT con duracion de 30 minutos
        [HttpPost("login")]
        public async Task<ActionResult<AutenticacionResponse>> Login([FromBody] NegocioCapa.DTOs.LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _usuarioService.AutenticarUsuario(loginRequest.Email, loginRequest.Contrasena);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }

            var token = _tokenHelper.GenerarToken(usuario);

            var response = new AutenticacionResponse
            {
                Usuario = usuario,
                Token = token
            };

            return Ok(response);
        }

    }
}
