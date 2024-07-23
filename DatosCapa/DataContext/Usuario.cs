using System;
using System.Collections.Generic;

namespace DatosCapa.DataContext;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int RolId { get; set; }

    public virtual ICollection<Dirección> Direccións { get; set; } = new List<Dirección>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Rol Rol { get; set; } = null!;
}
