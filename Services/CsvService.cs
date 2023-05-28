
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
namespace LifeEnsure.Services;
public class CsvService
{
    
public List<HeatmapData> ReadHeatmapDataFromCSV(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var heatmapData = new List<HeatmapData>();
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

                    var data = new HeatmapData { Lat = lat, Lng = lng, Value = value };
                    heatmapData.Add(data);
                }

                return heatmapData;
            }
}

}