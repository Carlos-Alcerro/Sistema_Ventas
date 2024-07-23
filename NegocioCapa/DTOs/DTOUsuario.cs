using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.DTOs
{
    
        public class DTOUsuario
        {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Sexo { get; set; }
            public string Email { get; set; }
            public string Contrasena { get; set; }
            public int? RolId { get; set; }
        }

}
