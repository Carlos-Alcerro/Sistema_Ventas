using DatosCapa.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioCapa.DTOs
{
    public class AutenticacionResponse
    {
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
    }
}
