
using CsvHelper;
using LifeEnsure.Data;
using LifeEnsure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
namespace LifeEnsure.Services;
public class CsvService
{
     private readonly TigreHacksContext _context;

        public CsvService(TigreHacksContext context)
        {
            _context = context;
        }
 public List<HeatmapDatum> ReadHeatmapDataFromCSV(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var heatmapData = new List<HeatmapDatum>();
                csv.Read();
                csv.ReadHeader();

                var isFirstRow = true; // Flag para identificar la primera fila

                while (csv.Read())
                {
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue; // Saltar la primera fila
                    }

                    var latLng = csv.GetField<string>(0).Split(',');
                    var lat = double.Parse(latLng[0]);
                    var lng = double.Parse(latLng[1]);
                    var value = csv.GetField<int>(1);

                    var data = new HeatmapDatum { Lat = lat, Lng = lng, Value = value };
                    heatmapData.Add(data);
                }

                return heatmapData;
            }
        }

        public void SaveHeatmapDataToDatabase(List<HeatmapDatum> heatmapData)
        {
            _context.HeatmapData.AddRange(heatmapData);
            _context.SaveChanges();
        }

        public async Task<HeatmapDatum> GetByLatitudLongitud(double latitud, double longitud)
{
    return await _context.HeatmapData.FirstOrDefaultAsync(d => d.Lat == latitud && d.Lng == longitud);
}

public async Task Update(HeatmapDatum heatmapDatum)
{
    _context.HeatmapData.Update(heatmapDatum);
    await _context.SaveChangesAsync();
}

public async Task Create(HeatmapDatum heatmapDatum)
{
    _context.HeatmapData.Add(heatmapDatum);
    await _context.SaveChangesAsync();
}
public List<HeatmapDatum> GetHeatmapDataFromDatabase()
{
    var heatmapData = _context.HeatmapData.ToList(); // Reemplaza "_dbContext" con tu contexto de base de datos

    return heatmapData;
}
    }


    







