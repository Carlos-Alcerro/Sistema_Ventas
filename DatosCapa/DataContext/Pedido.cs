using System;
using System.Collections.Generic;

namespace DatosCapa.DataContext;

public partial class Pedido
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int DirecciónId { get; set; }

    public DateTime Fecha { get; set; }

    public string? Producto { get; set; }

    public decimal Total { get; set; }

    public virtual Dirección Dirección { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
