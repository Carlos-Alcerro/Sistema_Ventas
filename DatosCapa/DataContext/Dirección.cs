using System;
using System.Collections.Generic;

namespace DatosCapa.DataContext;

public partial class Dirección
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string? Calle { get; set; }

    public string? Ciudad { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Usuario Usuario { get; set; } = null!;
}
