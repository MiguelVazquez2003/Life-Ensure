﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LifeEnsure.Data.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    [Range(0, 1, ErrorMessage = "El campo Licencia solo puede tener los valores 0 o 1.")]

    public int? Licencia { get; set; }

    public string? Genero { get; set; }

    public int? Edad { get; set; }

    [JsonIgnore]
    public virtual ICollection<Accidente> Accidentes { get; set; } = new List<Accidente>();
    [JsonIgnore]
    public virtual ICollection<Carro> Carros { get; set; } = new List<Carro>();
}
