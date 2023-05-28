using System;
using System.Collections.Generic;

namespace LifeEnsure.Data.Models;

public partial class Carro
{
    public int Id { get; set; }

    public string? Placas { get; set; }

    public int? Modelo { get; set; }

    public double? ValorDeducible { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<Accidente> Accidentes { get; set; } = new List<Accidente>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
