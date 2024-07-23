using DatosCapa.DataContext;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//Se creo esta funcion para poder generar un token de autenticacion
namespace SistemaPOS.Helpers
{
    public class TokenHelper
    {
        private readonly string _jwtSecret = "UnaClaveSecretaMuyLargaParaJWTQueTieneAlMenos32Caracteres!ksdkjfksaffksakfksadsadnksandkfnsafkdnfksdnfkfsadn";

        public string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
