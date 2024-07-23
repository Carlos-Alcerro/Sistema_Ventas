using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.DTOs
{
    public class DTOPedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int DirecciónId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Producto { get; set; }
        public decimal Total { get; set; }
    }
}
