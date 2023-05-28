using System;
using System.Collections.Generic;

namespace LifeEnsure.Data.Models;

public partial class HeatmapDatum
{
    public int Id { get; set; }

    public double Lat { get; set; }

    public double Lng { get; set; }

    public int Value { get; set; }
}
