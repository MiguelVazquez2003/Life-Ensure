﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LifeEnsure.Data.Models;

public partial class Accidente
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public string? TipoAccidente { get; set; }

    public string? Causa { get; set; }

    public string? TipoVialidad { get; set; }

    public string? NombreVialidad { get; set; }

    public string? Cruce { get; set; }

    public string? Sentido { get; set; }

    public string? Colonia { get; set; }

    public string? Municipio { get; set; }

    public decimal? Longitud { get; set; }

    public decimal? Latitud { get; set; }

    public int? IdVehiculo { get; set; }

    public int? IdUsuario { get; set; }

    public int? Lesionados { get; set; }

    public int? Muertos { get; set; }

    public string? SituacionClimatica { get; set; }

    public string? SituacionPavimento { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }

    [JsonIgnore]
    public virtual Carro? IdVehiculoNavigation { get; set; }
}
