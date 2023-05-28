using LifeEnsure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace LifeEnsure.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class CsvController : ControllerBase
    {
        private readonly CsvService _csvService;

        public CsvController(CsvService csvService)
        {
            _csvService = csvService;
        }

         [HttpGet("getall")]
    public IActionResult GetHeatmapData()
    {
        var filePath = "C:\\Users\\angel\\Downloads\\Mapa\\Mapa.csv"; // Especifica la ruta al archivo CSV
        var heatmapData = _csvService.ReadHeatmapDataFromCSV(filePath);
        return Ok(heatmapData);
    }
}
}

