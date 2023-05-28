using Microsoft.AspNetCore.Mvc;
using LifeEnsure.Services;
using LifeEnsure.Data.Models;
using LifeEnsure.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LifeEnsure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccidenteController : ControllerBase
    {
        private readonly AccidenteService _accidenteService;
        private readonly CsvService _csvService;

        public AccidenteController(AccidenteService accidenteService, CsvService csvService)
        {
            _accidenteService = accidenteService;
            _csvService=csvService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<AccidenteDtoOut>>> GetAll()
        {
            var accidentes = await _accidenteService.GetAll();
            return Ok(accidentes);
        }

        [HttpGet("getall/{id}")]
        public async Task<ActionResult<AccidenteDtoOut>> GetById(int id)
        {
            var accidente = await _accidenteService.GetById(id);
            if (accidente == null)
                return NotFound();
            
            return Ok(accidente);
        }

   [HttpPost("create")]
public async Task<IActionResult> Create(AccidenteDtoIn accidente)
{
    // Insertar el nuevo registro en la tabla "Accidente"
    var newAccidente = await _accidenteService.Create(accidente);

    // Obtener la latitud y longitud del nuevo registro
    double latitud = (double)accidente.Latitud;
    double longitud = (double)accidente.Longitud;

    // Verificar si existe un registro en la tabla "HeatmapDatum" con la misma latitud y longitud
    var existingHeatmapDatum = await _csvService.GetByLatitudLongitud(latitud, longitud);

    if (existingHeatmapDatum != null)
    {
        // Actualizar el campo "Recount" incrementando su valor en 1
        existingHeatmapDatum.Value++;
        await _csvService.Update(existingHeatmapDatum);
    }
    else
    {
        // Crear un nuevo registro en la tabla "HeatmapDatum" con la latitud, longitud y valor inicial de "Recount" como 1
        var newHeatmapDatum = new HeatmapDatum
        {
            Lat = latitud,
            Lng = longitud,
            Value = 1
        };
        await _csvService.Create(newHeatmapDatum);
    }

    return CreatedAtAction(nameof(GetById), new { id = newAccidente.Id }, newAccidente);
}


        [HttpPut("modificar/{id}")]
        public async Task<IActionResult> Update(int id, AccidenteDtoIn accidenteDTO)
        {
            var existingAccidente = await _accidenteService.GetById(id);
            if (existingAccidente == null)
                return NotFound();

            await _accidenteService.Update(id, accidenteDTO);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var accidente = await _accidenteService.GetById(id);
            if (accidente == null)
                return NotFound();

            await _accidenteService.Delete(id);

            return NoContent();
        }
    }
}
