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

        public AccidenteController(AccidenteService accidenteService)
        {
            _accidenteService = accidenteService;
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
        public async Task<ActionResult<AccidenteDtoOut>> Create(AccidenteDtoIn newAccidenteDTO)
        {
            var accidente = await _accidenteService.Create(newAccidenteDTO);
            return CreatedAtAction(nameof(GetById), new { id = accidente.Id }, accidente);
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
