using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.DTOs
{
    public class DTODireccion
    {
        public int UsuarioId { get; set; }
        public string? Calle { get; set; }
        public string? Ciudad { get; set; }
        public string? Estado { get; set; }
        public int Id { get; set; }
    }
}
